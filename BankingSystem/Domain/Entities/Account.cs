using BankingSystem.Domain.Exceptions;

namespace BankingSystem.Domain.Entities;

public class Account
{
    public string AccountNumber { get; set; }
    public int CustomerId { get; set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedDate { get; set; }
    public List<Transaction> TransactionHistory { get; private set; }

    public Account(string accountNumber, int customerId, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        CustomerId = customerId;
        Balance = initialBalance;
        CreatedDate = DateTime.Now;
        TransactionHistory = new List<Transaction>();
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive.");

        if (Balance < amount)
            throw new InsufficientFundsException(AccountNumber, amount, Balance);

        Balance -= amount;
    }

    public void AddTransaction(Transaction transaction)
    {
        TransactionHistory.Add(transaction);
    }
}
