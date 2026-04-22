using Server.Models;
using Shared.Enums;

namespace Server.World
{
    public sealed class WorldService
    {
        private readonly Dictionary<int, RoomDefinition> _rooms = new();
        private readonly Dictionary<int, HashSet<string>> _roomOccupants = new();
        private readonly Dictionary<string, int> _characterRooms = new();
        private readonly Dictionary<string, string> _characterNames = new();
        private readonly Dictionary<string, Direction> _characterDirections = new();

        public void Boot()
        {
            _rooms.Clear();
            _roomOccupants.Clear();
            _characterRooms.Clear();
            _characterNames.Clear();
            _characterDirections.Clear();

            AddRoom(new RoomDefinition
            {
                Id = 1,
                Name = "Town Square",
                Description = "You are standing in the town square. Old stone streets branch out in every direction.",
                Exits = new Dictionary<string, int>
                {
                    ["N"] = 2,
                    ["E"] = 3,
                    ["W"] = 4
                }
            });

            AddRoom(new RoomDefinition
            {
                Id = 2,
                Name = "North Gate",
                Description = "A heavy gate towers above the northern road. Guards keep a lazy watch nearby.",
                Exits = new Dictionary<string, int>
                {
                    ["S"] = 1
                }
            });

            AddRoom(new RoomDefinition
            {
                Id = 3,
                Name = "Market Street",
                Description = "Canvas stalls crowd the lane, though most of the merchants have already packed up for the day.",
                Exits = new Dictionary<string, int>
                {
                    ["W"] = 1
                }
            });

            AddRoom(new RoomDefinition
            {
                Id = 4,
                Name = "West Dock",
                Description = "Dark water laps against weathered pilings. The river smells of rain and old trade routes.",
                Exits = new Dictionary<string, int>
                {
                    ["E"] = 1
                }
            });

            Console.WriteLine($"[World] Booted {_rooms.Count} rooms.");
        }

        public bool HasRoom(int roomId) => _rooms.ContainsKey(roomId);

        public void EnterRoom(string characterId, string characterName, int roomId, Direction direction = Direction.South)
        {
            if (!_rooms.ContainsKey(roomId))
                throw new InvalidOperationException($"Room {roomId} does not exist.");

            LeaveRoom(characterId);

            _characterNames[characterId] = characterName;
            _characterDirections[characterId] = direction;
            _roomOccupants[roomId].Add(characterId);
            _characterRooms[characterId] = roomId;
        }

        public void LeaveRoom(string characterId)
        {
            if (_characterRooms.TryGetValue(characterId, out var roomId))
            {
                _roomOccupants[roomId].Remove(characterId);
                _characterRooms.Remove(characterId);
            }

            _characterNames.Remove(characterId);
            _characterDirections.Remove(characterId);
        }

        public int? GetCharacterRoom(string characterId)
            => _characterRooms.TryGetValue(characterId, out var roomId) ? roomId : null;

        public string? GetCharacterName(string characterId)
            => _characterNames.TryGetValue(characterId, out var name) ? name : null;

        public Direction? GetCharacterDirection(string characterId)
            => _characterDirections.TryGetValue(characterId, out var direction) ? direction : null;

        public IReadOnlyList<string> GetCharactersInRoom(int roomId)
        {
            if (!_roomOccupants.TryGetValue(roomId, out var occupants))
                return Array.Empty<string>();

            return occupants.ToList();
        }

        public bool TryMove(string characterId, Direction direction, out RoomSnapshot snapshot)
        {
            snapshot = null!;

            if (!_characterRooms.TryGetValue(characterId, out var currentRoomId))
                return false;

            if (!_rooms.TryGetValue(currentRoomId, out var currentRoom))
                return false;

            if (!_characterNames.TryGetValue(characterId, out var characterName))
                return false;

            var key = direction switch
            {
                Direction.North => "N",
                Direction.East => "E",
                Direction.South => "S",
                Direction.West => "W",
                Direction.Up => "U",
                Direction.Down => "D",
                _ => string.Empty
            };

            if (string.IsNullOrEmpty(key))
                return false;

            if (!currentRoom.Exits.TryGetValue(key, out var targetRoomId))
                return false;

            if (!_rooms.ContainsKey(targetRoomId))
                return false;

            LeaveRoom(characterId);
            EnterRoom(characterId, characterName, targetRoomId, direction);

            snapshot = BuildRoomSnapshot(targetRoomId);
            return true;
        }

        public RoomSnapshot BuildRoomSnapshot(int roomId)
        {
            if (!_rooms.TryGetValue(roomId, out var def))
                throw new InvalidOperationException($"Room {roomId} not found.");

            var occupants = _roomOccupants.TryGetValue(roomId, out var set)
                ? set.Select(id => _characterNames.TryGetValue(id, out var name) ? name : id)
                    .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                    .ToList()
                : new List<string>();

            return new RoomSnapshot
            {
                RoomId = def.Id,
                Name = def.Name,
                Description = def.Description,
                Exits = def.Exits.Keys.Select(ToExitDisplayName).ToList(),
                Players = occupants,
                Npcs = Array.Empty<string>(),
                Items = Array.Empty<string>()
            };
        }

        public void FastTick() { }
        public void SlowTick() { }
        public void SpawnTick() { }

        private void AddRoom(RoomDefinition room)
        {
            _rooms[room.Id] = room;
            _roomOccupants[room.Id] = new HashSet<string>(StringComparer.Ordinal);
        }

        private static string ToExitDisplayName(string exitKey) => exitKey.ToUpperInvariant() switch
        {
            "N" => "North",
            "E" => "East",
            "S" => "South",
            "W" => "West",
            "U" => "Up",
            "D" => "Down",
            _ => exitKey
        };
    }
}
