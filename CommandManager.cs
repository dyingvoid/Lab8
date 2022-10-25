using System.Numerics;

namespace Lab8;

public class CommandManager
{
    private List<Command> _commandList;

    public List<Command> CommandList => _commandList;

    public CommandManager(string[] fileContent)
    {
        if (HasStartBalance(fileContent[0]))
        {
            _commandList = new List<Command>();
            ConvertFileContentToCommands(fileContent);
            
            //Commands can have the same time, how do we sort???
            _commandList.Sort(CompareCommands);
        }
        else
        {
            Console.WriteLine("Wrong file format. Start balance must be positive integer value.");
            _commandList = new List<Command>();
        }
    }

    private void ConvertFileContentToCommands(string[] fileContent)
    {
        for (int i = 0; i < fileContent.Length; ++i)
        {
            string[] commandProperties = fileContent[i].Split(new char[] { ' ', '|' },
                StringSplitOptions.RemoveEmptyEntries);
            
            AddCommandToList(commandProperties);
        }
    }

    private void AddCommandToList(string[] commandProperties)
    {
        if (IsRightCommandLength(commandProperties, new int[3] { 1, 3, 4 }))
        {
            _commandList.Add(new Command(commandProperties));
        }
        else
        {
            Console.WriteLine("Wrong command length.");
        }
    }

    private bool HasStartBalance(string commandProperty)
    {
        BigInteger checkInputBalance;
        bool answer = BigInteger.TryParse(commandProperty, out checkInputBalance) 
                      && checkInputBalance >= 0;

        return answer;
    }

    private bool IsRightCommandLength(string[] commandProperties, int[] commandLengths)
    {
        bool answer = false;
        foreach (var len in commandLengths)
        {
            answer = answer || len == commandProperties.Length;
        }

        return answer;
    }

    private int CompareCommands(Command x, Command y)
    {
        DateTime xDateTime = x.DateTime;
        DateTime yDateTime = y.DateTime;
        return DateTime.Compare(xDateTime, yDateTime);
    }
    
    public void PrintContent()
    {
        foreach (var command in _commandList)
        {
            Console.WriteLine(command.DateTime.ToString() + " " + command.Amount + " " + command.Type);
        }
        Console.WriteLine();
    }
}