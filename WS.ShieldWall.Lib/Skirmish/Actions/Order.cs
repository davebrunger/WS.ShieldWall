namespace WS.ShieldWall.Lib.Skirmish.Actions;

public record Order
(
    string CommanderName,
    string UnitName,
    Location Destination,
    bool Charge
);
