namespace WS.HailCaesar.Lib.Models;

public enum UnitSubType
{
    Baggage,
    Cataphracts,
    Elephants,
    HeavyArtillery,
    HeavyCavalry,
    HeavyChariots,
    HeavyInfantry,
    HorseArchers,
    LightArtillery,
    LightCavalry,
    LightChariots,
    LightInfantry,
    MediumArtillery,
    MediumCavalry,
    MediumInfantry,
    Skirmishers,
}

public static class UnitSubTypeExtensions
{
    public static UnitType GetUnitType(this UnitSubType unitSubType)
    {
        return unitSubType switch
        {
            UnitSubType.Baggage => UnitType.Baggage,
            UnitSubType.Cataphracts => UnitType.Cavalry,
            UnitSubType.Elephants => UnitType.Elephants,
            UnitSubType.HeavyArtillery => UnitType.Artillery,
            UnitSubType.HeavyCavalry => UnitType.Cavalry,
            UnitSubType.HeavyChariots => UnitType.Chariots,
            UnitSubType.HeavyInfantry => UnitType.Infantry,
            UnitSubType.HorseArchers => UnitType.Cavalry,
            UnitSubType.LightArtillery => UnitType.Artillery,
            UnitSubType.LightCavalry => UnitType.Cavalry,
            UnitSubType.LightChariots => UnitType.Chariots,
            UnitSubType.LightInfantry => UnitType.Infantry,
            UnitSubType.MediumArtillery => UnitType.Artillery,
            UnitSubType.MediumCavalry => UnitType.Cavalry,
            UnitSubType.MediumInfantry => UnitType.Infantry,
            UnitSubType.Skirmishers => UnitType.Infantry,
            _ => throw new ArgumentOutOfRangeException(nameof(unitSubType)),
        };
    }
    
    public static UnitCategory GetUnitCategory(this UnitSubType unitSubType)
    {
        return unitSubType switch
        {
            UnitSubType.Baggage => UnitCategory.Skirmishers,
            UnitSubType.Cataphracts => UnitCategory.Cavalry,
            UnitSubType.Elephants => UnitCategory.Cavalry,
            UnitSubType.HeavyArtillery => UnitCategory.Infantry,
            UnitSubType.HeavyCavalry => UnitCategory.Cavalry,
            UnitSubType.HeavyChariots => UnitCategory.Cavalry,
            UnitSubType.HeavyInfantry => UnitCategory.Infantry,
            UnitSubType.HorseArchers => UnitCategory.Cavalry,
            UnitSubType.LightArtillery => UnitCategory.Infantry,
            UnitSubType.LightCavalry => UnitCategory.Cavalry,
            UnitSubType.LightChariots => UnitCategory.Cavalry,
            UnitSubType.LightInfantry => UnitCategory.Infantry,
            UnitSubType.MediumArtillery => UnitCategory.Infantry,
            UnitSubType.MediumCavalry => UnitCategory.Cavalry,
            UnitSubType.MediumInfantry => UnitCategory.Infantry,
            UnitSubType.Skirmishers => UnitCategory.Skirmishers,
            _ => throw new ArgumentOutOfRangeException(nameof(unitSubType)),
        };
    }
}
