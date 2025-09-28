using UnityEngine;
using System;

[Serializable]
public class Stat_DefenseGroup
{
    //Physical defense
    public Stats armor;
    public Stats evasion;

    //Elemental defense
    public Stats fireRes;
    public Stats iceRes;
    public Stats lightningRes;
}
