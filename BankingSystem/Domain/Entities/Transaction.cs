using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public abstract class Transaction
{
    public int TransactionId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; }

    protected Transaction(string accountNumber, decimal amount, TransactionType type, string description)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        Type = type;
        Description = description;
        TransactionDate = DateTime.Now;
    }

    public abstract string GetTransactionDetails();
}
