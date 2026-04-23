namespace Server.Models
{
    public sealed class NpcInstance
    {
        public required string InstanceId { get; init; }
        public required string NpcId { get; init; }
        public int RoomId { get; set; }
        public int CurrentHP { get; set; }
    }
}
