using Server.Database;
using Server.Game;
using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using Shared.Security;
using System;
using System.IO;
using System.Text;

namespace Server.Networking
{
    public sealed class DataHandler
    {
        private readonly ServerGameLogic _logic;
        private readonly ServerTcp _tcp;

        public DataHandler(ServerGameLogic logic, ServerTcp tcp)
        {
            _logic = logic;
            _tcp = tcp;
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

                        var usernameOrEmail = br.ReadString();
                        var password = br.ReadString();
                        var clientVersion = br.ReadString();

                        var req = new LoginRequestDto
                        {
                            UsernameOrEmail = usernameOrEmail,
                            Password = password,
                            ClientVersion = clientVersion
                        };

                        var res = _logic.HandleLogin(req);

                        _tcp.SendLoginOk(clientId, res.Success, res.Message ?? "", res.AccountId ?? "");
                        if (res.Success && res.AccountId is not null)
                        {
                            _tcp.SetAccount(clientId, res.AccountId);
                            var chars = _logic.GetCharacters(res.AccountId);
                            _tcp.SendAllChars(clientId, chars);
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

                        _tcp.SetCharacter(clientId, charId);
                        _tcp.SendInGame(clientId); // enter game
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

                        var character = _logic.CreateCharacter(
                            accountUsername,
                            name,
                            classId,
                            gender,
                            avatar,
                            out var error
                        );

                        if (character == null)
                        {
                            _tcp.SendAlert(clientId, error ?? "Failed to create character.");
                            break;
                        }

                        var list = _logic.GetCharacters(accountUsername);
                        _tcp.SendAllChars(clientId, list);
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

                        var list = _logic.GetCharacters(accountId);
                        _tcp.SendAllChars(clientId, list);
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

                        var summaries = _logic.GetCharacters(account.Username);
                        _tcp.SendAllChars(clientId, summaries);
                        break;
                    }
            }
        }
    }
}