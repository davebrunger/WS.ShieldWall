namespace WS.ShieldWall.Lib.Skirmish.Actions;

public record InitativeMove
(
    string UnitName,
    Location Destination,
    bool Charge
);