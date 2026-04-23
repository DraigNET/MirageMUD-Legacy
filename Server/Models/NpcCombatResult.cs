namespace Server.Models
{
    public sealed class NpcCombatResult
    {
        public required string InstanceId { get; init; }
        public required string DisplayName { get; init; }
        public required string BaseName { get; init; }
        public required int RoomId { get; init; }
        public int Damage { get; init; }
        public int RemainingHP { get; init; }
        public bool Died { get; init; }
    }
}
