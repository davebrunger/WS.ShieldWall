namespace WS.ShieldWall;

public static class Math
{
    public static (int X, int Y) Rotate(int x, int y)
    {
        return (y, -x);
    }
    
    public static (int X, int Y) Rotate(this (int X, int Y) point)
    {
        return Rotate(point.X, point.Y);
    }
}
