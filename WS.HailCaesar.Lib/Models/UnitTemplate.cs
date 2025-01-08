namespace WS.HailCaesar.Lib.Models;

public record UnitTemplate
(
    string Name,
    string Description,
    UnitSubType SubType,
    IImmutableSet<Weapon> Weapons,
    int Clash,
    int Sustained,
    int Support,
    int ShortRange,
    int LongRange,
    int MoraleSave,
    int Stamina,
    int PointsPerUnit,
    IImmutableSet<Special> Special
)
{
    public int MaxRange => Weapons.Count > 0 ? Weapons.Max(w => w.GetMaxRange()) : 0;
}
