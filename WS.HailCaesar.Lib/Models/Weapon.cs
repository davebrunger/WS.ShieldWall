namespace WS.HailCaesar.Lib.Models;

public enum Weapon
{
    Bow,
    Crossbow,
    Dart,
    HeavyArtillery,
    Javelin,
    LightArtillery,
    MediumArtillery,
    Sling,
    StaffSling,
    Thrown,
}

public static class WeaponExtensions
{
    public static int GetMaxRange(this Weapon weapon)
    {
        return weapon switch
        {
            Weapon.Bow => 18,
            Weapon.Crossbow => 18,
            Weapon.Dart => 6,
            Weapon.HeavyArtillery => 48,
            Weapon.Javelin => 6,
            Weapon.LightArtillery => 24,
            Weapon.MediumArtillery => 36,
            Weapon.Sling => 12,
            Weapon.StaffSling => 18,
            Weapon.Thrown => 6,
            _ => 0
        };
    }
}
