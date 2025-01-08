namespace WS.HailCaesar.Lib.Models;

/// <summary>
/// The formation a unit is currently in
/// </summary>
/// <remarks>
/// Warband has been implented as a <see cref="Special">Special Rule</see> rather than a Formation
/// </remarks>
public enum UnitFormation
{
    BattleLine,
    Column,
    OpenOrder,
    PikePhalanx,
    PigsHead,
    Square,
    Testudo,
    Wedge
}

public static class UnitFormationExtensions
{
    public static int GetMaxRangedDice(this UnitFormation unitFormation)
    {
        return unitFormation switch
        {
            UnitFormation.Column => 0,
            UnitFormation.Square => 1,
            UnitFormation.Testudo => 0,
            _ => int.MaxValue
        };
    }
    public static int GetMaxHandToHandDice(this UnitFormation unitFormation, int squareSides = 0)
    {
        return unitFormation switch
        {
            UnitFormation.Column => 1,
            UnitFormation.Square => squareSides * 2,
            _ => int.MaxValue
        };
    }

    public static bool IsFormed(this UnitFormation unitFormation)
    {
        return unitFormation != UnitFormation.OpenOrder;
    }
}
