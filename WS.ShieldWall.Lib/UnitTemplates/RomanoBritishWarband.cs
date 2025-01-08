namespace WS.ShieldWall.Lib.UnitTemplates;

public static class RomanoBritishWarband
{
    public static readonly UnitTemplate Dux = new(
        "Dux",
        "Commander, equiped with sword, shield and metal armour",
        UnitSubType.HeavyInfantry,
        ImmutableHashSet<Weapon>.Empty,
        3,
        2,
        0,
        0,
        0,
        4,
        3,
        15,
        ImmutableHashSet<Special>.Empty.Add(Special.Elite).Add(Special.Commander));

    public static readonly UnitTemplate GermanFeoderatus = new(
        "German Feoderatus",
        "Leader, equiped with sword, shield and metal armour",
        UnitSubType.HeavyInfantry,
        ImmutableHashSet<Weapon>.Empty,
        4,
        2,
        0,
        0,
        0,
        5,
        2,
        12,
        ImmutableHashSet<Special>.Empty.Add(Special.Elite).Add(Special.Leader));

    public static readonly UnitTemplate Ceorl = new(
        "Warrior",
        "Equiped with spear, shield and leather armour",
        UnitSubType.MediumInfantry,
        ImmutableHashSet<Weapon>.Empty,
        2,
        1,
        0,
        0,
        0,
        6,
        1,
        5,
        ImmutableHashSet<Special>.Empty);


    public static readonly UnitTemplate Cavalryman = new(
        "Cavalryman",
        "Equiped with sword, leather armour and javelins",
        UnitSubType.MediumCavalry,
        ImmutableHashSet<Weapon>.Empty.Add(Weapon.Javelin),
        2,
        1,
        1,
        1,
        0,
        5,
        1,
        8,
        ImmutableHashSet<Special>.Empty.Add(Special.Elite));

    public static readonly UnitTemplate Javelinman = new(
        "Javelinman",
        "Equiped with javelins",
        UnitSubType.Skirmishers,
        ImmutableHashSet<Weapon>.Empty.Add(Weapon.Javelin),
        2,
        1,
        1,
        1,
        0,
        0,
        1,
        5,
        ImmutableHashSet<Special>.Empty.Add(Special.Levy));

    public static readonly UnitTemplate Farmer = new(
        "Farmer",
        "Equiped with improvised weapon",
        UnitSubType.Skirmishers,
        ImmutableHashSet<Weapon>.Empty.Add(Weapon.Javelin),
        1,
        1,
        0,
        0,
        0,
        0,
        1,
        2,
        ImmutableHashSet<Special>.Empty.Add(Special.Levy));
}
