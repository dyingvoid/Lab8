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
        _windowWidth = 50 + largestCommandPhrase;
        _windowHeight = 25;
    }

    public void Draw(List<Command> commandList)
    {
        Dictionary<Tuple<int, int>, Command> commandDict = ProcessCommands(commandList);

        PrintHorizontalBorder(true);
        for (int i = 0; i < _windowHeight; ++i)
        {
            Console.Write(_verticalSymbol);
            
            PrintContent(commandDict, i);
            
            Console.WriteLine(_verticalSymbol);
        }
        PrintHorizontalBorder(false);
    }

    private void PrintContent(Dictionary<Tuple<int, int>, Command> commandDict, int i)
    {
        for (int j = 0; j < _windowWidth - 2; ++j)
        {
            j += PrintStroke(commandDict, j, i);
            Console.Write(' ');
        }
    }

    private static int PrintStroke(Dictionary<Tuple<int, int>, Command> commandDict, int j, int i)
    {
        Command temp;
        commandDict.TryGetValue(Tuple.Create(j, i), out temp);

        if (temp is not null)
        {
            Console.ForegroundColor = temp.Colour;
            Console.Write(temp.Phrase);
            Console.ResetColor();
            return temp.Phrase.Length;
        }

        return 0;
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

    private Dictionary<Tuple<int, int>, Command> ProcessCommands(List<Command> commandList)
    {
        var dict = new Dictionary<Tuple<int, int>, Command>();
        foreach (var command in commandList)
        {
            Tuple<int, int> posXY = WhereToPut(command);
            dict.Add(posXY, command);
        }
        //MAKE CHEEEEEEEEEEEEEEEEEEEEEEEECKS!
        return dict;
    }
    private int HowManyLinesTextTakes(string text)
    {
        return text.Length / _windowWidth;
    }

    private Tuple<int, int> WhereToPut(Command command)
    {
        // Dictionary Pos Position coords X, Y
        var posXY = new Dictionary<Pos, Tuple<int, int>>()
        {
            {Pos.Top,  Tuple.Create(_windowWidth / 2, 0) },
            {Pos.Left, Tuple.Create(1, _windowHeight / 2) },
            {Pos.Right, Tuple.Create(_windowWidth - command.Phrase.Length - 3, _windowHeight / 2) },
            {Pos.Bottom, Tuple.Create(_windowWidth / 2, _windowHeight - 1)}
        };
        
        //MAKE SOME CHECKS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        return posXY[command.Position];
    }

    public void ClearConsole()
    {
        Console.Clear();
    }
}