namespace Server.Models
{
    public sealed class RoomDefinition
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Exits keyed by direction string (N, S, E, W, U, D)
        /// Value is destination room id.
        /// </summary>
        public Dictionary<string, int> Exits { get; set; } = new();
    }
}