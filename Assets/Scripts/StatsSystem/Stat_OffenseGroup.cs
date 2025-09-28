using System;

[Serializable]
public class Stat_OffenseGroup
{
    public Stats attackSpeed;

    // Physical damage
    public Stats damage;
    public Stats critPower;
    public Stats critChance;
    public Stats armorReduction;

    // Elemental damage
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightningDamage;
}
