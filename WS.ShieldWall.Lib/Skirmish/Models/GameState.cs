namespace WS.ShieldWall.Lib.Skirmish.Models
{
    public record GameState
    (
        int TurnNumber,
        int CurrentPlayerIndex,
        SubPhase SubPhase
    );
}
