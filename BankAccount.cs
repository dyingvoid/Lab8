using System.Numerics;

namespace Lab8;

public class BankAccount
{
    private BigInteger _balance;
    private bool _acccountCreated;
    private Stack<BigInteger> _operations;

    public BankAccount(List<Command> commandList)
    {
        _acccountCreated = false;
        _operations = new Stack<BigInteger>();
        
        foreach (var command in commandList)
        {
            Console.Write(_balance.ToString() + " ");
            OperateBalance(DefineOperation(command), command.Amount);
            Console.WriteLine(_balance.ToString());
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
    
    private bool CreateAccount(BigInteger amount)
    {
        if (_acccountCreated || amount < 0)
        {
            Console.WriteLine("Account already created.");
            return false;
        }
        
        _balance = amount;
        _acccountCreated = true;
        return true;
    }

    public void OperateBalance(Func<BigInteger, bool> balanceOperation, BigInteger amount)
    {
        var tempBalance = _balance;
        if (balanceOperation(amount))
        {
            _operations.Push(_balance - tempBalance);
        }
    }
    private bool Deposit(BigInteger amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Wrong amount in Deposit. Must be bigger than 0");
            return false;
        }
        
        _balance += amount;
        return true;
    }

    private bool WithDraw(BigInteger amount)
    {
        if (amount > _balance)
        {
            Console.WriteLine($"Not enough balance({_balance}) to withdraw {amount}. Deposit {amount - _balance}.");
            return false;
        }

        _balance -= amount;
        return true;    
    }

    private bool ErrorOperation(BigInteger amount)
    {
        Console.WriteLine($"Error operation called. {amount} was met.");
        return false;
    }

    private bool Revert(BigInteger amount)
    {
        BigInteger lastOperationAmount;
        try
        {
            lastOperationAmount = _operations.Peek();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("_operations stack is empty.");
            return false;
        }

        if (lastOperationAmount > 0)
        {
            return WithDraw(lastOperationAmount);
        }
        else if (lastOperationAmount < 0)
        {
            return Deposit(BigInteger.Abs(lastOperationAmount));
        }

        return ErrorOperation(lastOperationAmount);
    }
}