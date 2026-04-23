using Server.Game;
using Server.Models;
using Server.World;
using Shared.Enums;
using Shared.Networking;
using System.Text;

namespace Server.Networking
{
    public sealed class DataHandler
    {
        private readonly ServerGameLogic _logic;
        private readonly ServerTcp _tcp;
        private readonly EnterGameService _enterGame;
        private readonly WorldService _world;

        public DataHandler(ServerGameLogic logic, ServerTcp tcp, WorldService world)
        {
            _logic = logic;
            _tcp = tcp;
            _world = world;
            _enterGame = new EnterGameService(logic, world);
        }

        public void HandlePacket(int clientId, ClientPacketId packetId, ReadOnlySpan<byte> payload)
        {
            switch (packetId)
            {
                case ClientPacketId.CSync:
                {
                    _tcp.SendSync(clientId, "pong");
                    break;
                }

                case ClientPacketId.CLogin:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var req = new LoginRequestDto
                    {
                        UsernameOrEmail = br.ReadString(),
                        Password = br.ReadString(),
                        ClientVersion = br.ReadString()
                    };

                    var res = _logic.HandleLogin(req);

                    _tcp.SendLoginOk(clientId, res.Success, res.Message ?? string.Empty, res.AccountId ?? string.Empty);
                    if (res.Success && res.AccountId is not null)
                    {
                        _tcp.SetAccount(clientId, res.AccountId);
                        _tcp.SendAllChars(clientId, _logic.GetCharacters(res.AccountId));
                    }

                    break;
                }

                case ClientPacketId.CUseChar:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var charId = br.ReadString();

                    if (!_tcp.TryGetAccount(clientId, out var accountId) || accountId is null)
                    {
                        _tcp.SendAlert(clientId, "Not logged in.");
                        break;
                    }

                    if (!_logic.CharacterBelongsTo(accountId, charId))
                    {
                        _tcp.SendAlert(clientId, "That character is not yours.");
                        break;
                    }

                    if (_tcp.IsCharacterActive(charId, clientId))
                    {
                        _tcp.SendAlert(clientId, "That character is already in use.");
                        break;
                    }

                    RoomSnapshot snapshot;

                    try
                    {
                        snapshot = _enterGame.Enter(accountId, charId);
                    }
                    catch (Exception ex)
                    {
                        _tcp.SendAlert(clientId, ex.Message);
                        break;
                    }

                    _tcp.SetCharacter(clientId, charId);
                    _tcp.SendInGame(clientId);
                    SendPlayerState(clientId, accountId, charId);
                    _tcp.SendRoomSnapshot(clientId, snapshot);

                    var name = _world.GetCharacterName(charId) ?? "Someone";
                    BroadcastRoomMessage(snapshot.RoomId, "Server", $"{name} enters the area.", clientId);
                    break;
                }

                case ClientPacketId.CAddChar:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var name = br.ReadString();
                    var classId = (CharacterClass)br.ReadInt32();
                    var gender = (Gender)br.ReadInt32();
                    var avatar = br.ReadInt32();

                    if (!_tcp.TryGetAccount(clientId, out var accountUsername) || accountUsername is null)
                    {
                        _tcp.SendAlert(clientId, "Not logged in.");
                        break;
                    }

                    var character = _logic.CreateCharacter(accountUsername, name, classId, gender, avatar, out var error);
                    if (character == null)
                    {
                        _tcp.SendAlert(clientId, error ?? "Failed to create character.");
                        break;
                    }

                    _tcp.SendAllChars(clientId, _logic.GetCharacters(accountUsername));
                    break;
                }

                case ClientPacketId.CDelChar:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var charId = br.ReadString();

                    if (!_tcp.TryGetAccount(clientId, out var accountId) || accountId is null)
                    {
                        _tcp.SendAlert(clientId, "Not logged in.");
                        break;
                    }

                    if (!_logic.DeleteCharacter(accountId, charId))
                    {
                        _tcp.SendAlert(clientId, "Delete failed.");
                        break;
                    }

                    _tcp.SendAllChars(clientId, _logic.GetCharacters(accountId));
                    break;
                }

                case ClientPacketId.CNewAccount:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var username = br.ReadString();
                    var email = br.ReadString();
                    var password = br.ReadString();

                    var account = _logic.CreateAccount(username, email, password, out var error);
                    if (account == null)
                    {
                        _tcp.SendAlert(clientId, error);
                        break;
                    }

                    _tcp.SetAccount(clientId, account.Username);
                    _tcp.SendLoginOk(clientId, true, "Account created.", account.Username);
                    _tcp.SendAllChars(clientId, _logic.GetCharacters(account.Username));
                    break;
                }

                case ClientPacketId.CPlayerMove:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var direction = (Direction)br.ReadInt32();

                    if (!_tcp.TryGetCharacter(clientId, out var characterId) || characterId is null)
                    {
                        _tcp.SendAlert(clientId, "Not in-game.");
                        break;
                    }

                    string? accountId = null;
                    _tcp.TryGetAccount(clientId, out accountId);

                    HandleMovement(clientId, accountId, characterId, direction);
                    break;
                }

                case ClientPacketId.CSayMsg:
                {
                    using var ms = new MemoryStream(payload.ToArray());
                    using var br = new BinaryReader(ms, Encoding.UTF8);

                    var text = br.ReadString().Trim();

                    if (!_tcp.TryGetCharacter(clientId, out var characterId) || characterId is null)
                    {
                        _tcp.SendAlert(clientId, "Not in-game.");
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(text))
                        break;

                    var cmd = text.ToLowerInvariant();
                    var direction = cmd switch
                    {
                        "n" or "north" => Direction.North,
                        "e" or "east" => Direction.East,
                        "s" or "south" => Direction.South,
                        "w" or "west" => Direction.West,
                        _ => (Direction?)null
                    };

                    if (direction.HasValue)
                    {
                        string? accountId = null;
                        _tcp.TryGetAccount(clientId, out accountId);

                        HandleMovement(clientId, accountId, characterId, direction.Value);
                        break;
                    }

                    if (cmd is "look" or "l")
                    {
                        var roomId = _world.GetCharacterRoom(characterId);
                        if (!roomId.HasValue)
                        {
                            _tcp.SendAlert(clientId, "You are nowhere.");
                            break;
                        }

                        _tcp.SendRoomSnapshot(clientId, _world.BuildRoomSnapshot(roomId.Value));
                        break;
                    }

                    HandlePlayerSay(characterId, text);
                    break;
                }
            }
        }

        public void HandleDisconnect(int clientId, string? accountId, string? characterId)
        {
            if (string.IsNullOrWhiteSpace(characterId))
                return;

            var roomId = _world.GetCharacterRoom(characterId);
            var name = _world.GetCharacterName(characterId) ?? "Someone";

            if (roomId.HasValue && !string.IsNullOrWhiteSpace(accountId))
            {
                var direction = _world.GetCharacterDirection(characterId) ?? Direction.South;
                _logic.UpdateCharacterLocation(accountId, characterId, roomId.Value, direction);
            }

            _world.LeaveRoom(characterId);

            if (roomId.HasValue)
                BroadcastRoomMessage(roomId.Value, "Server", $"{name} has disconnected.");
        }

        private void HandleMovement(int clientId, string? accountId, string characterId, Direction direction)
        {
            var fromRoomId = _world.GetCharacterRoom(characterId);
            var name = _world.GetCharacterName(characterId) ?? "Someone";

            if (!_world.TryMove(characterId, direction, out var snapshot))
            {
                _tcp.SendAlert(clientId, "You can't go that way.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(accountId))
                _logic.UpdateCharacterLocation(accountId, characterId, snapshot.RoomId, direction);

            _tcp.SendRoomSnapshot(clientId, snapshot);

            if (fromRoomId.HasValue)
            {
                BroadcastRoomMessage(
                    fromRoomId.Value,
                    "Server",
                    $"{name} leaves to the {DirectionToText(direction)}.",
                    clientId);
            }

            BroadcastRoomMessage(
                snapshot.RoomId,
                "Server",
                $"{name} arrives from the {DirectionToText(GetOppositeDirection(direction))}.",
                clientId);
        }

        private void HandlePlayerSay(string characterId, string text)
        {
            var roomId = _world.GetCharacterRoom(characterId);
            if (!roomId.HasValue)
                return;

            var name = _world.GetCharacterName(characterId) ?? "Someone";
            var targetClientIds = _tcp.GetClientIdsForCharacters(_world.GetCharactersInRoom(roomId.Value));

            foreach (var targetClientId in targetClientIds)
                _tcp.SendSayMsg(targetClientId, name, text);
        }

        private void BroadcastRoomMessage(int roomId, string from, string message, int? exceptClientId = null)
        {
            var targetClientIds = _tcp.GetClientIdsForCharacters(_world.GetCharactersInRoom(roomId));

            foreach (var targetClientId in targetClientIds)
            {
                if (exceptClientId.HasValue && targetClientId == exceptClientId.Value)
                    continue;

                _tcp.SendSayMsg(targetClientId, from, message);
            }
        }

        private void SendPlayerState(int clientId, string accountId, string characterId)
        {
            var character = _logic.GetCharacter(accountId, characterId);
            if (character == null)
                return;

            var nextLevelExperience = _logic.GetNextLevelExperience(character);
            var stats = _logic.GetDerivedStats(character);
            var hp = _logic.GetVital(character, VitalType.HP);
            var mp = _logic.GetVital(character, VitalType.Mana);
            var stamina = _logic.GetVital(character, VitalType.Stamina);

            _tcp.SendPlayerData(clientId, character, nextLevelExperience);
            _tcp.SendPlayerStats(clientId, stats.Strength, stats.Defense, stats.Magi, stats.Speed, stats.CritHit, stats.BlockChance);
            _tcp.SendPlayerHp(clientId, hp.Current, hp.Max);
            _tcp.SendPlayerMp(clientId, mp.Current, mp.Max);
            _tcp.SendPlayerStamina(clientId, stamina.Current, stamina.Max);
        }

        private static Direction GetOppositeDirection(Direction direction) => direction switch
        {
            Direction.North => Direction.South,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            Direction.West => Direction.East,
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            _ => direction
        };

        private static string DirectionToText(Direction direction) => direction switch
        {
            Direction.North => "north",
            Direction.East => "east",
            Direction.South => "south",
            Direction.West => "west",
            Direction.Up => "up",
            Direction.Down => "down",
            _ => "somewhere"
        };
    }
}
