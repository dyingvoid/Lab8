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
            AddBalanceToBalanceHistory(command);
            
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
        
        if (!IsAmountPositive(amount) || !balanceOperation(amount))
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
    
    /// <summary>
    /// Is amount bigger than 0
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private bool IsAmountPositive(BigInteger amount)
    {
        return amount >= 0;
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
    
    private void AddBalanceToBalanceHistory(Command command)
    {
        _timeBalanceHistory[command.DateTime] = _currentBalance;
    }

    public BigInteger CheckBalanceAtTime(DateTime time)
    {
        DateTime tempTime;
        BigInteger tempBalance = new BigInteger(-1);

        if (!DoesAccountExist() || !DidAccountExistAtTime(time))
        {
            tempBalance = new BigInteger(-1);
            return tempBalance;
        }

        tempBalance = FindBalanceAtTime(time, tempBalance);
        
        return tempBalance;
    }

    private BigInteger FindBalanceAtTime(DateTime time, BigInteger tempBalance)
    {
        if (time > _timeBalanceHistory.Last().Key)
            return _currentBalance;
        
        foreach (var (timeHistory, balance) in _timeBalanceHistory)
        {
            if (timeHistory == time)
            {
                tempBalance = balance;
                break;
            }
            else if (timeHistory < time)
            {
                tempBalance = balance;
            }
        }

        return tempBalance;
    }

    private bool DoesAccountExist()
    {
        if (_timeBalanceHistory.Count == 0)
        {
            Console.WriteLine("Account does not exist.");
            return false;
        }

        return true;
    }
    
    private bool DidAccountExistAtTime(DateTime time)
    {
        if (time < _timeBalanceHistory.First().Key)
        {
            Console.WriteLine("Account did not exist.");
            return false;
        }
        
        return true;
    }
}