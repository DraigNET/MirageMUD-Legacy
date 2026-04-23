namespace Server.Models
{
    public sealed class PendingNpcRespawn
    {
        public required int RoomId { get; init; }
        public required string NpcId { get; init; }
        public required DateTime RespawnAtUtc { get; init; }
    }
}
