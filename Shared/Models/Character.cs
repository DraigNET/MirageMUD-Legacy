using Shared.Enums;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public sealed class Character
    {
        // Identity
        public string Id { get; init; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; } = "";
        public int ClassId { get; set; }   // stored in JSON
        public int Level { get; set; } = 1;

        // Computed property: never stored, just convenience
        public CharacterClass Class => (CharacterClass)ClassId;
        public Gender Gender { get; set; }
        public int Avatar { get; set; }
        public long Experience { get; set; } = 0;

        public int AccessLevel { get; set; } = 0; // admin/gm flags
        public bool IsPK { get; set; } = false;   // player killer flag

        public string Guild { get; set; } = string.Empty;
        public int GuildAccess { get; set; } = 0;

        // Vitals (HP, Mana, Stamina, etc.)
        public int[] Vitals { get; set; } = new int[Enum.GetValues<VitalType>().Length];

        // Helpers for vitals
        public int CurrentHP
        {
            get => Vitals[(int)VitalType.HP];
            set => Vitals[(int)VitalType.HP] = value;
        }

        public int CurrentMana
        {
            get => Vitals[(int)VitalType.Mana];
            set => Vitals[(int)VitalType.Mana] = value;
        }

        public int CurrentStamina
        {
            get => Vitals[(int)VitalType.Stamina];
            set => Vitals[(int)VitalType.Stamina] = value;
        }

        // Stats (Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, etc.)
        public int[] Stats { get; set; } = new int[Enum.GetValues<StatType>().Length];

        // Helpers for stats
        public int Strength
        {
            get => Stats[(int)StatType.Strength];
            set => Stats[(int)StatType.Strength] = value;
        }

        public int Dexterity
        {
            get => Stats[(int)StatType.Dexterity];
            set => Stats[(int)StatType.Dexterity] = value;
        }

        public int Constitution
        {
            get => Stats[(int)StatType.Constitution];
            set => Stats[(int)StatType.Constitution] = value;
        }

        public int Intelligence
        {
            get => Stats[(int)StatType.Intelligence];
            set => Stats[(int)StatType.Intelligence] = value;
        }

        public int Wisdom
        {
            get => Stats[(int)StatType.Wisdom];
            set => Stats[(int)StatType.Wisdom] = value;
        }

        public int Charisma
        {
            get => Stats[(int)StatType.Charisma];
            set => Stats[(int)StatType.Charisma] = value;
        }

        public int UnspentStatPoints { get; set; } = 0;

        // Equipment slots (Weapon, Armor, Helmet, Shield, etc.)
        public int[] Equipment { get; set; } = new int[Enum.GetValues<EquipmentSlot>().Length];

        // Inventory
        public List<PlayerInventoryItem> Inventory { get; set; } = new();

        // Known spells (list of spell IDs)
        public List<int> Spells { get; set; } = new();

        // Position
        public int RoomId { get; set; }
        public int Direction { get; set; } // later: enum Direction
    }

    public sealed class PlayerInventoryItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Durability { get; set; }
    }
}