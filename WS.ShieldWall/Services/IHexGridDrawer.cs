namespace WS.ShieldWall.Services;

public interface IHexGridDrawer
{
    void DrawGrid(int width, int height, Func<int, int, int, string> getText);
}