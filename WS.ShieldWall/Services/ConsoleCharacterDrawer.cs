namespace WS.ShieldWall.Services;

public class ConsoleCharacterDrawer : ICharacterDrawer
{
    public void Draw(string str)
    {
        Console.Write(str);
    }

    public void DrawUnderlined(string str)
    {
        Draw(Underline(str));
    }

    public void NewLine()
    {
        Console.WriteLine();
    }

    private static string Underline(string text)
    {
        return $"\x1B[4m{text}\x1B[0m";
    }
}
