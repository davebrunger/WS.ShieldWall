namespace WS.ShieldWall.Lib.UnitTemplates;

public static class SaxonWarband
{
    public static readonly UnitTemplate Chief = new(
        "Chief",
        "Commander, equiped with sword, shield and metal armour",
        UnitSubType.HeavyInfantry,
        ImmutableHashSet<Weapon>.Empty,
        4,
        2,
        0,
        0,
        0,
        4,
        3,
        16,
        ImmutableHashSet<Special>.Empty.Add(Special.Brave).Add(Special.Commander));

    public static readonly UnitTemplate Gesith = new(
        "Gesith",
        "Leader, equiped with spear, shield and leather armour",
        UnitSubType.MediumInfantry,
        ImmutableHashSet<Weapon>.Empty,
        3,
        2,
        0,
        0,
        0,
        5,
        2,
        11,
        ImmutableHashSet<Special>.Empty.Add(Special.Brave).Add(Special.Leader));

    public static readonly UnitTemplate Ceorl = new(
        "Ceorl",
        "Equiped with spear and shield",
        UnitSubType.LightInfantry,
        ImmutableHashSet<Weapon>.Empty,
        2,
        1,
        0,
        0,
        0,
        6,
        1,
        6,
        ImmutableHashSet<Special>.Empty.Add(Special.Brave));

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
        ImmutableHashSet<Special>.Empty);

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
