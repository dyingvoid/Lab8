using System.Data;
using System.Numerics;

namespace Lab8;

public enum OperationType
{
    Create,
    In,
    Out,
    Revert,
    Wrong
}

public class Command
{
    private DateTime _dateTime;
    private BigInteger _amount;
    private OperationType _type;
    private bool IsGood;

    public DateTime DateTime => _dateTime;
    public BigInteger Amount => _amount;
    public OperationType Type => _type;

    /// <summary>
    /// There are three types of commands.
    /// IsGood points whether object was initialized with no errors.
    /// </summary>
    /// <param name="command"></param>
    public Command(string[] command)
    {
        switch (command.Length)
        {
            case 1:
                IsGood = SetInitialAmount(command[0]) &&
                         SetOperationType("Create");
                break;
            case 3:
                IsGood = SetDateTime(command[0], command[1]) &&
                         SetOperationType(command[2]);
                break;
            case 4:
                IsGood = SetDateTime(command[0], command[1]) &&
                         SetAmount(command[2]) &&
                         SetOperationType(command[3]);
                break;
            default:
                Console.WriteLine("Wrong command length.");
                break;
        }
    }

    // Initial amount can be 0 and positive number
    private bool SetInitialAmount(string amount)
    {
        BigInteger integerAmount;
        if (BigInteger.TryParse(amount, out integerAmount) && integerAmount >= 0)
        {
            _amount = integerAmount;
            return true;
        }
        
        Console.WriteLine("Error in account creating command. First line of file must be positive integer value.");
        return false;
    }

    // Types of commands described in OperationType enum
    private bool SetOperationType(string type)
    {
        type = UpFirstLetter(type);
        
        if (!Enum.TryParse(type, out _type))
        {
            _type = OperationType.Wrong;
            Console.WriteLine($"{type} Operation type does not exist." +
                              $"Operation type is set to wrong");
            return false;
        }

        return true;
    }

    private static string UpFirstLetter(string type)
    {
        try
        {
            type = type[0].ToString().ToUpper() + type.Substring(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} at UpFirstLetter().");
        }

        return type;
    }

    /// <summary>
    /// method is only used for OperationType.In and Out,
    /// amount must be bigger than 0
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private bool SetAmount(string amount)
    {
        BigInteger bigIntegerAmount;
        if (!BigInteger.TryParse(amount, out bigIntegerAmount) || bigIntegerAmount <= 0)
        {
            Console.WriteLine($"Amount({amount}) must be bigger than 0.");
            return false;
        }

        _amount = bigIntegerAmount;
        return true;
    }

    private bool SetDateTime(string date, string time)
    {
        string dateTime = date + " " + time;
        try
        {
            _dateTime = DateTime.ParseExact(dateTime, "yyyy-MM-dd hh:mm",
                System.Globalization.CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            _dateTime = DateTime.MinValue;
            Console.WriteLine($"Date({date}) or time({time}) is wrong. " +
                              $"_dateTime is set to {DateTime.MinValue}");
            return false;
        }

        return true;
    }
    
    
}