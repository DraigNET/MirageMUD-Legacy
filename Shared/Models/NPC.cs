namespace Shared.Models
{
    public sealed class NPC
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public int Level { get; init; } = 1;
        public int MaxHP { get; init; } = 10;
        public int MinDamage { get; init; } = 1;
        public int MaxDamage { get; init; } = 3;
        public int RespawnSeconds { get; init; } = 10;
    }
}
