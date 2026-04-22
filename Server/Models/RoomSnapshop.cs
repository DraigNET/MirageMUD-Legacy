namespace Server.Models
{
    public sealed class RoomSnapshot
    {
        public int RoomId { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// Available exits from this room (N, S, E, W, etc.)
        /// </summary>
        public IReadOnlyList<string> Exits { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Character names currently visible in the room.
        /// (Later: include self flag, ids, etc.)
        /// </summary>
        public IReadOnlyList<string> Players { get; init; } = Array.Empty<string>();

        /// <summary>
        /// NPC display names currently in the room.
        /// </summary>
        public IReadOnlyList<string> Npcs { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Item display names currently in the room.
        /// </summary>
        public IReadOnlyList<string> Items { get; init; } = Array.Empty<string>();
    }
}