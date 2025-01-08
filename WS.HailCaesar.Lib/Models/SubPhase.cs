namespace WS.HailCaesar.Lib.Models;

public enum SubPhase
{
    InitiativeMoves,
    Orders,
    FreeMoves
}

public static class SubPhaseExtensions
{
    public static Phase GetPhase(this SubPhase phase)
    {
        return phase switch
        {
            SubPhase.InitiativeMoves => Phase.Command,
            SubPhase.Orders => Phase.Command,
            SubPhase.FreeMoves => Phase.Command,

            _ => throw new ArgumentOutOfRangeException(nameof(phase))
        };
    }
}
