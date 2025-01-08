namespace WS.HailCaesar.Lib.Services;

public class DieRoller : IDieRoller
{
    private static readonly Random random = new();

    public int Roll()
    {
        return random.Next(1, 7);
    }
}
