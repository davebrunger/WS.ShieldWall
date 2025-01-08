namespace WS.ShieldWall.Services;

public class HexGridDrawer : IHexGridDrawer
{
    private readonly ICharacterDrawer characterDrawer;

    public HexGridDrawer(ICharacterDrawer characterDrawer)
    {
        this.characterDrawer = characterDrawer;
    }

    public void DrawGrid(int width, int height, Func<int, int, int, string> getText)
    {
        string getTrimmedText(int x, int y, int row)
        {
            return getText(x, y, row).PadRight(2)[..2];
        }

        characterDrawer.Draw(" ");
        for (int x = 0; x < width; x++)
        {
            if (x % 2 == 0)
            {
                characterDrawer.Draw("__");
            }
            else
            {
                characterDrawer.Draw("    ");
            }
        }
        characterDrawer.NewLine();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x % 2 == 0)
                {
                    characterDrawer.Draw($"/{getTrimmedText(x, y, 0)}");
                }
                else
                {
                    if (y > 0)
                    {
                        characterDrawer.Draw("\\");
                        characterDrawer.DrawUnderlined(getTrimmedText(x, y - 1, 1));
                    }
                    else
                    {
                        characterDrawer.Draw("\\__");
                    }
                }
            }
            if (width % 2 == 0)
            {
                if (y > 0)
                {
                    characterDrawer.Draw("/");
                }
            }
            else
            {
                characterDrawer.Draw("\\");
            }
            characterDrawer.NewLine();
            for (int x = 0; x < width; x++)
            {
                if (x % 2 == 0)
                {
                    characterDrawer.Draw("\\");
                    characterDrawer.DrawUnderlined(getTrimmedText(x, y, 1));
                }
                else
                {
                    characterDrawer.Draw($"/{getTrimmedText(x, y, 0)}");
                }
            }
            if (width % 2 == 0)
            {
                characterDrawer.Draw("\\");
            }
            else
            {
                characterDrawer.Draw("/");
            }
            characterDrawer.NewLine();
        }
        for (int x = 0; x < width; x++)
        {
            if (x == 0)
            {
                characterDrawer.Draw("   ");
            }
            else if (x % 2 == 0)
            {
                characterDrawer.Draw("/  ");
            }
            else
            {
                characterDrawer.Draw("\\");
                characterDrawer.DrawUnderlined(getTrimmedText(x, height - 1, 1));
            }
        }
        if (width % 2 == 0)
        {
            characterDrawer.Draw("/");
        }
        characterDrawer.NewLine();
    }
}
