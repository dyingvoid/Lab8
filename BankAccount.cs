using System.Numerics;

namespace Lab8;

public class BankAccount
{
    private BigInteger _currentBalance;
    private bool _accountCreated;
    private BigInteger _lastOperationAmount;
    private SortedDictionary<DateTime, BigInteger> _timeBalanceHistory;

    public BankAccount(List<Command> commandList)
    {
        _accountCreated = false;
        _timeBalanceHistory = new SortedDictionary<DateTime, BigInteger>();

        foreach (var command in commandList)
        {
            //commandList can contain commands with the same time and uses default sort by time
            Console.Write(_currentBalance.ToString() + " ");
            OperateBalance(DefineOperation(command), command.Amount);
            Console.WriteLine(_currentBalance.ToString());
        }
    }

    private Func<BigInteger, bool> DefineOperation(Command command)
    {
        OperationType operationType = command.Type;

        switch (operationType)
        {
            case OperationType.Create:
                return CreateAccount;
            case OperationType.In:
                return Deposit;
            case OperationType.Out:
                return WithDraw;
            case OperationType.Revert:
                return Revert;
            case OperationType.Wrong:
                Console.WriteLine("Wrong operation met.");
                return ErrorOperation;
            default:
                Console.WriteLine($"Unknown operation type met: {command.Type}.");
                return ErrorOperation;
        }
    }
    
    public void OperateBalance(Func<BigInteger, bool> balanceOperation, BigInteger amount)
    {
        var tempBalance = _currentBalance;
        if (!balanceOperation(amount))
        {
            Console.WriteLine("Error while operating balance.");
            return;
        }

        //positive for deposit, negative for withdraw
        _lastOperationAmount = _currentBalance - tempBalance;
        
    }
    
    private bool CreateAccount(BigInteger amount)
    {
        if (_accountCreated || amount < 0)
        {
            Console.WriteLine("Account already created.");
            return false;
        }
        
        _currentBalance = amount;
        _accountCreated = true;
        return true;
    }
    
    private bool Deposit(BigInteger amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Wrong amount in Deposit. Must be bigger than 0");
            return false;
        }
        
        _currentBalance += amount;
        return true;
    }

    private bool WithDraw(BigInteger amount)
    {
        if (amount > _currentBalance)
        {
            Console.WriteLine($"Not enough balance({_currentBalance}) to withdraw {amount}." +
                              $" Deposit {amount - _currentBalance} to withdraw.");
            return false;
        }

        _currentBalance -= amount;
        return true;    
    }

    private bool ErrorOperation(BigInteger amount)
    {
        Console.WriteLine($"Error operation called. {amount} was met.");
        return false;
    }

    private bool Revert(BigInteger amount)
    {
        if (_lastOperationAmount > 0)
        {
            return WithDraw(_lastOperationAmount);
        }
        else if (_lastOperationAmount < 0)
        {
            return Deposit(BigInteger.Abs(_lastOperationAmount));
        }

        return ErrorOperation(_lastOperationAmount);
    }
}