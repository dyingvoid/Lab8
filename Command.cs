using System.Data;
using System.Numerics;

namespace Lab8;

enum OperationType
{
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
    public bool IsGood;

    public Command(string[] command)
    {
        switch (command.Length)
        {
            case 1:
                IsGood = CreateAccount(command[0]);
                break;
            case 3:
                IsGood = SetDateTime(command[0], command[1]) &&
                         SetOperationType(command[3]);
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

    private bool CreateAccount(string amount)
    {
        BigInteger integerAmount;
        if (BigInteger.TryParse(amount, out integerAmount) && integerAmount >= 0)
        {
            _amount = integerAmount;
            return true;
        }
        
        Console.WriteLine("Error while creating account. First line of file must be positive integer value.");
        return false;
    }

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