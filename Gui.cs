namespace Lab8;

public class Gui
{
    private int _windowWidth;
    private int _windowHeight;
    private char _horizontalSymbol;
    private char _verticalSymbol;

    public Gui(char horizontalSymbol, char verticalSymbol,
        int largestCommandPhrase)
    {
        _horizontalSymbol = horizontalSymbol;
        _verticalSymbol = verticalSymbol;
        SetScreenSize(largestCommandPhrase);
    }

    private void SetScreenSize(int largestCommandPhrase)
    {
        _windowWidth = 50;
        _windowHeight = 25;
    }

    public void Draw(List<Command> commandList)
    {
        PrintHorizontalBorder(true);
        for (int i = 0; i < _windowHeight; ++i)
        {
            Console.Write(_verticalSymbol);
            for (int j = 0; j < _windowWidth - 2; ++j)
            {
                Console.Write(' ');
            }
            Console.WriteLine(_verticalSymbol);
        }

        PrintHorizontalBorder(false);
    }

    public void PrintHorizontalBorder(bool isTop)
    {
        for (int i = 0; i < _windowWidth; ++i)
        {
            Console.Write(_horizontalSymbol);
        }
        if(isTop)
            Console.WriteLine();
    }

    private void ProcessCommands(List<Command> commandList)
    {
        foreach (var command in commandList)
        {
            Console.WriteLine(HowManyLinesTextTakes(command.Phrase));
        }
    }
    private int HowManyLinesTextTakes(string text)
    {
        return text.Length / _windowWidth;
    }

    public void ClearConsole()
    {
        Console.Clear();
    }
}