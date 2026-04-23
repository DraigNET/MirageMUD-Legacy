using Server.Models;
using Shared.Enums;
using Shared.Models;

namespace Server.World
{
    public sealed class WorldService
    {
        private readonly WorldRepository _repository;
        private readonly Dictionary<int, RoomDefinition> _rooms = new();
        private readonly Dictionary<string, NPC> _npcDefinitions = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, NpcInstance> _npcInstances = new(StringComparer.Ordinal);
        private readonly Dictionary<int, HashSet<string>> _roomOccupants = new();
        private readonly Dictionary<int, HashSet<string>> _roomNpcs = new();
        private readonly Dictionary<string, int> _characterRooms = new();
        private readonly Dictionary<string, string> _characterNames = new();
        private readonly Dictionary<string, Direction> _characterDirections = new();
        private int _nextNpcInstanceId;

        public WorldService(WorldRepository repository)
        {
            _repository = repository;
        }

        public void Boot()
        {
            _rooms.Clear();
            _npcDefinitions.Clear();
            _npcInstances.Clear();
            _roomOccupants.Clear();
            _roomNpcs.Clear();
            _characterRooms.Clear();
            _characterNames.Clear();
            _characterDirections.Clear();
            _nextNpcInstanceId = 0;

            foreach (var room in _repository.LoadRooms())
                AddRoom(room);

            foreach (var npc in _repository.LoadNpcs())
                _npcDefinitions[npc.Id] = npc;

            SpawnMissingRoomNpcs();

            Console.WriteLine($"[World] Booted {_rooms.Count} rooms and {_npcDefinitions.Count} NPC templates.");
        }

        public bool HasRoom(int roomId) => _rooms.ContainsKey(roomId);
        public int DefaultRoomId => _rooms.Keys.OrderBy(id => id).FirstOrDefault(1);

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

        public IReadOnlyList<NpcInstanceView> GetNpcsInRoom(int roomId)
        {
            if (!_roomNpcs.TryGetValue(roomId, out var npcIds))
                return Array.Empty<NpcInstanceView>();

            var baseEntries = npcIds
                .Select(id => BuildNpcEntry(id))
                .Where(entry => entry != null)
                .Select(entry => entry!.Value)
                .OrderBy(entry => entry.BaseName, StringComparer.OrdinalIgnoreCase)
                .ThenBy(entry => entry.InstanceId, StringComparer.Ordinal)
                .ToList();

            if (baseEntries.Count == 0)
                return Array.Empty<NpcInstanceView>();

            var counters = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var totals = baseEntries
                .GroupBy(entry => entry.BaseName, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

            var views = new List<NpcInstanceView>(baseEntries.Count);
            foreach (var entry in baseEntries)
            {
                counters.TryGetValue(entry.BaseName, out var currentCount);
                currentCount++;
                counters[entry.BaseName] = currentCount;

                var displayName = totals[entry.BaseName] > 1
                    ? $"{entry.BaseName} ({currentCount})"
                    : entry.BaseName;

                views.Add(new NpcInstanceView
                {
                    InstanceId = entry.InstanceId,
                    DisplayName = displayName
                });
            }

            return views;
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
                Npcs = GetNpcsInRoom(roomId),
                Items = Array.Empty<string>()
            };
        }

        public void FastTick() { }
        public void SlowTick() { }
        public void SpawnTick() => SpawnMissingRoomNpcs();

        private void AddRoom(RoomDefinition room)
        {
            _rooms[room.Id] = room;
            _roomOccupants[room.Id] = new HashSet<string>(StringComparer.Ordinal);
            _roomNpcs[room.Id] = new HashSet<string>(StringComparer.Ordinal);
        }

        private void SpawnMissingRoomNpcs()
        {
            foreach (var room in _rooms.Values)
            {
                foreach (var spawn in room.NpcSpawns)
                {
                    if (!_npcDefinitions.TryGetValue(spawn.NpcId, out var npcDefinition))
                        continue;

                    var existingCount = _roomNpcs[room.Id]
                        .Select(id => _npcInstances.TryGetValue(id, out var instance) ? instance : null)
                        .Count(instance => instance != null && string.Equals(instance.NpcId, npcDefinition.Id, StringComparison.OrdinalIgnoreCase));

                    var needed = Math.Max(0, spawn.Count - existingCount);
                    for (var i = 0; i < needed; i++)
                        SpawnNpc(room.Id, npcDefinition);
                }
            }
        }

        private void SpawnNpc(int roomId, NPC definition)
        {
            var instanceId = $"npc_{roomId}_{++_nextNpcInstanceId:D4}";
            var instance = new NpcInstance
            {
                InstanceId = instanceId,
                NpcId = definition.Id,
                RoomId = roomId,
                CurrentHP = definition.MaxHP
            };

            _npcInstances[instanceId] = instance;
            _roomNpcs[roomId].Add(instanceId);
        }

        private (string InstanceId, string BaseName)? BuildNpcEntry(string instanceId)
        {
            if (!_npcInstances.TryGetValue(instanceId, out var instance))
                return null;

            if (!_npcDefinitions.TryGetValue(instance.NpcId, out var definition))
                return null;

            return (instance.InstanceId, definition.Name);
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
