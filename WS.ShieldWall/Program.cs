using SimpleHexes;
using WS.ShieldWall.Collections;
using WS.ShieldWall.Services;
using WS.ShieldWall;

var characterDrawer = new ConsoleCharacterDrawer();
var hexGridDrawer = new HexGridDrawer(characterDrawer);

var width = 20;
var height = 20;

var selectedHex = new Hex(1, 3);

var messages = new LastNStack<string>(5);

while (true)
{
    Console.Clear();

    Console.WriteLine("Shield Wall by David Brunger");
    Console.WriteLine();

    hexGridDrawer.DrawGrid(width, height, (x, y, r) =>
    {
        var hex = ToHex((x - (width - 1), y));
        if (hex == selectedHex)
        {
            return "++";
        }
        if (hex.GetDistanceTo(selectedHex) < 3)
        {
            return "--";
        }
        return string.Empty;
    });

    Console.WriteLine();
    Console.WriteLine(selectedHex);
    Console.WriteLine(ToGrid(selectedHex));
    Console.WriteLine(ToHex(ToGrid(selectedHex)));

    Console.WriteLine();
    foreach (var message in messages)
    {
        Console.WriteLine(message);
    }    

    var keyPressed = Console.ReadKey();

    Hex? newHex = keyPressed.Key switch
    {
        ConsoleKey.Insert => selectedHex.GetNeighbour(OrthogonalDirection.DownLeft),
        ConsoleKey.Home => selectedHex.GetNeighbour(OrthogonalDirection.Left),
        ConsoleKey.PageUp => selectedHex.GetNeighbour(OrthogonalDirection.UpLeft),
        ConsoleKey.Delete => selectedHex.GetNeighbour(OrthogonalDirection.DownRight),
        ConsoleKey.End => selectedHex.GetNeighbour(OrthogonalDirection.Right),
        ConsoleKey.PageDown => selectedHex.GetNeighbour(OrthogonalDirection.UpRight),
        _ => null
    };

    selectedHex = newHex;
}

Hex ToHex((int X, int Y) point)
{
    var (ptx, pty) = (point.X, point.Y).Rotate();
    var q = ptx - ((pty + 1) / 2);
    var r = pty;
    return new Hex(q, r);
}

(int X, int Y) ToGrid(Hex hex)
{
    var ptx = hex.Q + ((hex.R + 1) / 2);
    var pty = hex.R;
    return (ptx, pty).Rotate().Rotate().Rotate();
}

bool IsInGrid(Hex hex)
{
    var (x, y) = ToGrid(hex);
    return x > 0 && x < width && y > 0 && y < height;
}
