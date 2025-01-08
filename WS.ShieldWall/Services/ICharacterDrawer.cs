namespace WS.ShieldWall.Services;

public interface ICharacterDrawer
{
    void Draw(string str);
    void DrawUnderlined(string str);
    void NewLine();
}