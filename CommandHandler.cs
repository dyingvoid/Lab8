namespace Lab8;

public class CommandHandler
{
    private Dictionary<DateTime, List<Command>> _dCommands;
    public Dictionary<DateTime, List<Command>> DCommands => _dCommands;

    public CommandHandler(string[] fileContent)
    {
        _dCommands = new Dictionary<DateTime, List<Command>>();
        
        GetCommandsFromFileContent(fileContent);
    }
    
    public void GetCommandsFromFileContent(string[] fileContent)
    {
        for (int i = 0; i < fileContent.Length; i++)
        {
            Command command = new Command(fileContent[i]);
            AddCommandToDict(command);
        }
    }

    public void AddCommandToDict(Command command)
    {
        Command.TimeRange range = command.TimeOfRendering;
        for (DateTime i = range.Start; i <= range.End; i += System.TimeSpan.FromSeconds(1))
        {
            List<Command> lCommand = new List<Command>(){command};
            if (!_dCommands.TryAdd(i, lCommand))
            {
                _dCommands[i].Add(command);
            }
        }
    }

    public void PrintCommands()
    {
        int counter = 1;
        foreach (var (key, value) in _dCommands)
        {
            Console.WriteLine($"{counter}.");
            foreach (var command in value)
            {
                command.GetInf();
                Console.Write(" ");
            }

            counter++;
            Console.WriteLine();
        }
    }
}