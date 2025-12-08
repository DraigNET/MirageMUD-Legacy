using System.Collections.Generic;
using Shared.Enums;

namespace Shared.Data
{
    public static class ClassDefinitions
    {
        // Base stats per class (Str, Dex, Con, Int, Wis, Cha)
        public static readonly Dictionary<CharacterClass, (int Str, int Dex, int Con, int Int, int Wis, int Cha)> Stats =
            new()
            {
                { CharacterClass.Warrior, (5, 3, 4, 1, 1, 2) },
                { CharacterClass.Mage,    (1, 2, 2, 5, 3, 2) },
                { CharacterClass.Cleric,  (2, 2, 3, 3, 4, 3) },
            };
    }
}