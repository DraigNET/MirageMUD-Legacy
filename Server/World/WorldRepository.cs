using Server.Models;
using Shared.Models;
using System.Text.Json;

namespace Server.World
{
    public sealed class WorldRepository
    {
        private readonly string _roomsPath;
        private readonly string _npcsPath;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public WorldRepository(string dataDir)
        {
            var worldDir = Path.Combine(dataDir, "World");
            Directory.CreateDirectory(worldDir);
            _roomsPath = Path.Combine(worldDir, "rooms.json");
            _npcsPath = Path.Combine(worldDir, "npcs.json");
        }

        public IReadOnlyList<RoomDefinition> LoadRooms()
        {
            if (!File.Exists(_roomsPath))
            {
                var defaults = CreateDefaultRooms();
                SaveRooms(defaults);
                return defaults;
            }

            var json = File.ReadAllText(_roomsPath);
            var rooms = JsonSerializer.Deserialize<List<RoomDefinition>>(json, _options);

            if (rooms == null || rooms.Count == 0)
            {
                rooms = CreateDefaultRooms();
                SaveRooms(rooms);
            }

            return rooms;
        }

        public IReadOnlyList<NPC> LoadNpcs()
        {
            if (!File.Exists(_npcsPath))
            {
                var defaults = CreateDefaultNpcs();
                SaveNpcs(defaults);
                return defaults;
            }

            var json = File.ReadAllText(_npcsPath);
            var npcs = JsonSerializer.Deserialize<List<NPC>>(json, _options);

            if (npcs == null || npcs.Count == 0)
            {
                npcs = CreateDefaultNpcs();
                SaveNpcs(npcs);
            }

            return npcs;
        }

        private void SaveRooms(IReadOnlyList<RoomDefinition> rooms)
        {
            var json = JsonSerializer.Serialize(rooms, _options);
            File.WriteAllText(_roomsPath, json);
        }

        private void SaveNpcs(IReadOnlyList<NPC> npcs)
        {
            var json = JsonSerializer.Serialize(npcs, _options);
            File.WriteAllText(_npcsPath, json);
        }

        private static List<RoomDefinition> CreateDefaultRooms() =>
            new()
            {
                new RoomDefinition
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
                },
                new RoomDefinition
                {
                    Id = 2,
                    Name = "North Gate",
                    Description = "A heavy gate towers above the northern road. Guards keep a lazy watch nearby.",
                    Exits = new Dictionary<string, int>
                    {
                        ["S"] = 1
                    },
                    NpcSpawns =
                    {
                        new NpcSpawnDefinition { NpcId = "snake", Count = 3 }
                    }
                },
                new RoomDefinition
                {
                    Id = 3,
                    Name = "Market Street",
                    Description = "Canvas stalls crowd the lane, though most of the merchants have already packed up for the day.",
                    Exits = new Dictionary<string, int>
                    {
                        ["W"] = 1
                    },
                    NpcSpawns =
                    {
                        new NpcSpawnDefinition { NpcId = "rat", Count = 2 }
                    }
                },
                new RoomDefinition
                {
                    Id = 4,
                    Name = "West Dock",
                    Description = "Dark water laps against weathered pilings. The river smells of rain and old trade routes.",
                    Exits = new Dictionary<string, int>
                    {
                        ["E"] = 1
                    }
                }
            };

        private static List<NPC> CreateDefaultNpcs() =>
            new()
            {
                new NPC
                {
                    Id = "snake",
                    Name = "Snake",
                    Level = 1,
                    MaxHP = 8,
                    MinDamage = 1,
                    MaxDamage = 2,
                    RespawnSeconds = 10
                },
                new NPC
                {
                    Id = "rat",
                    Name = "Rat",
                    Level = 1,
                    MaxHP = 6,
                    MinDamage = 1,
                    MaxDamage = 1,
                    RespawnSeconds = 8
                }
            };
    }
}
