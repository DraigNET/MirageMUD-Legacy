namespace Shared.Enums
{
    // Core stats for characters, NPCs, and classes
    public enum StatType
    {
        Strength,       // physical power, melee damage
        Dexterity,      // agility, dodge, crits
        Constitution,   // endurance, HP growth
        Intelligence,   // arcane power, spell damage
        Wisdom,         // healing, mana regen, resistances
        Charisma        // optional: NPC reactions, guild/social effects
    }

    // Resource pools
    public enum VitalType
    {
        HP,        // health
        Mana,      // magical energy
        Stamina    // physical energy for melee attacks
    }

    // Equipment slots (expandable later)
    public enum EquipmentSlot
    {
        Weapon,
        Armor,
        Helmet,
        Shield
    }
}