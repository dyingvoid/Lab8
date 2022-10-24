using System.Numerics;

namespace Lab8;

public class BankAccount
{
    private BigInteger _balance;
    private Stack<BigInteger> _operations;

    public BankAccount(BigInteger? balance = null)
    {
        balance ??= new BigInteger?(0);
        _balance = 0;
    }

    public void OperateBalance(Func<BigInteger, bool> balanceOperation, BigInteger amount)
    {
        var tempBalance = _balance;
        if (balanceOperation(amount))
        {
            _operations.Push(_balance - tempBalance);
        }
    }
    public bool Deposit(BigInteger amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Wrong amount in Deposit. Must be bigger than 0");
            return false;
        }
        
        _balance += amount;
        return true;
    }

    public bool WithDraw(BigInteger amount)
    {
        if (amount > _balance)
        {
            Console.WriteLine($"Not enough balance({_balance}) to withdraw {amount}. Deposit {amount - _balance}.");
            return false;
        }

        _balance -= amount;
        return true;    
    }
}