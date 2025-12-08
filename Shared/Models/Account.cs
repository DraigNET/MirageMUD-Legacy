namespace Shared.Models
{
    public sealed class Account
    {
        public required string Id { get; init; }               // GUID as string
        public required string Email { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }      // PBKDF2 format: pbkdf2|v=1|iter|salt|hash
        public bool EmailVerified { get; set; }
        public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;
        public List<Character> Characters { get; set; } = new();
    }
}