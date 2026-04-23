using Shared.Models;

namespace Server.Models
{
    public sealed class RoomSnapshot
    {
        public int RoomId { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public IReadOnlyList<string> Exits { get; init; } = Array.Empty<string>();

        public IReadOnlyList<string> Players { get; init; } = Array.Empty<string>();

        public IReadOnlyList<NpcInstanceView> Npcs { get; init; } = Array.Empty<NpcInstanceView>();

        public IReadOnlyList<string> Items { get; init; } = Array.Empty<string>();
    }
}
