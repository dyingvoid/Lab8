namespace Lab8;

public class Gui
{
    private int _windowHeight;
    private int _windowWidth;
    private Command[] _commands;
    private Dictionary<Command.TimeRange, List<Command>> _dCommands;

    public Gui(int height, int width, string[] fileContent)
    {
        if(height >= 0 && width >= 0)
        {
            _windowHeight = height;
            _windowWidth = width;
        }
        
        Command.TimeRangeSameRange timeRangeComp = new Command.TimeRangeSameRange();
        _dCommands = new Dictionary<Command.TimeRange, List<Command>>(timeRangeComp);
        
        _ = GetCommandsFromFile(fileContent);
    }

    public bool GetCommandsFromFile(string[] fileContent)
    {
        _commands = new Command[fileContent.Length];
        for (int i = 0; i < _commands.Length; i++)
        {
            Command command = new Command(fileContent[i]);
            command.GetInf();
            AddCommandToDict(command);
            _commands[i] = command;
        }
        PrintCommands();
        return true;
    }

    public void AddCommandToDict(Command command)
    {
        Command.TimeRange range = command._timeOfRendering;
        List<Command> lCommand = new List<Command>(){command};
        if (!_dCommands.TryAdd(range, lCommand))
        {
            _dCommands[range].Add(command);
        }
    }

    public void PrintCommands()
    {
        foreach (var (key, value) in _dCommands)
        {
            foreach (var command in value)
            {
                command.GetInf();
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}