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

    /// <summary>
    /// Prints top and bottom border
    /// if isTop true, prints additional \n symbol
    /// </summary>
    /// <param name="isTop">is border on top</param>
    public void PrintHorizontalBorder(bool isTop)
    {
        for (int i = 0; i < _windowWidth; ++i)
        {
            Console.Write(_horizontalSymbol);
        }
        if(isTop)
            Console.WriteLine();
    }

    /// <summary>
    /// Turn list of commands to dict with (command x,y coordinates: command)
    /// </summary>
    /// <param name="commandList">List of Command</param>
    /// <returns>Dict(x,y of command : command)</returns>
    private Dictionary<Tuple<int, int>, Command> ProcessCommands(List<Command> commandList)
    {
        var dict = new Dictionary<Tuple<int, int>, Command>();
        try
        {
            foreach (var command in commandList)
            {
                Tuple<int, int> posXY = PosToCoordinatesTuple(command);
                dict.Add(posXY, command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} in Gui.ProcessCommands()");
        }

        return dict;
    }
    private int HowManyLinesTextTakes(string text)
    {
        return text.Length / _windowWidth;
    }

    /// <summary>
    /// Turns Command.Pos to coordinates fot Gui class
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Tuple x,y coordinates</returns>
    private Tuple<int, int> PosToCoordinatesTuple(Command command)
    {
        // Dictionary Pos, position coords tuple X, Y
        var posXY = new Dictionary<Pos, Tuple<int, int>>()
        {
            {Pos.Top,  Tuple.Create(_windowWidth / 2 - 1, 0) },
            {Pos.Left, Tuple.Create(1, _windowHeight / 2 - 1) },
            {Pos.Right, Tuple.Create(_windowWidth - command.Phrase.Length - 3, _windowHeight / 2 - 1) },
            {Pos.Bottom, Tuple.Create(_windowWidth / 2 - 1, _windowHeight - 1)}
        };

        try
        {
            return posXY[command.Position];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} in Gui.PosToCoordinatesTuple(), return is Tuple({0, 0})");
            return Tuple.Create(0, 0);
        }
    }

    public void ClearConsole()
    {
        Console.Clear();
    }
}