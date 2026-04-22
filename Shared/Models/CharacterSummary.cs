using Shared.Enums;

namespace Shared.Models
{
    public sealed class CharacterSummary
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public int Level { get; init; }
        public int ClassId { get; init; }   // only this goes over the wire
        public int GenderId { get; init; }

        public string ClassName => ((CharacterClass)ClassId).ToString(); // UI only
        public string GenderName => ((Gender)GenderId).ToString();

        public static CharacterSummary FromCharacter(Character c)
            => new CharacterSummary
            {
                Id = c.Id,
                Name = c.Name,
                Level = c.Level,
                ClassId = c.ClassId,
                GenderId = (int)c.Gender
            };
    }
}
