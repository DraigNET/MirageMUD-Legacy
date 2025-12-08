using System.Collections.Generic;
using Shared.Enums;

namespace Shared.Models
{
    public sealed class ClassTemplate
    {
        public int Id { get; init; }             // e.g., 0 = Warrior, 1 = Mage, etc.
        public string Name { get; init; } = "";
        public int Avatar { get; init; }         // sprite index

        public Dictionary<StatType, int> Stats { get; init; } = new();
        public Dictionary<VitalType, int> Vitals { get; init; } = new();
    }
}