namespace FinanceTracker.Models;

public class Transaction
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public string Category { get; init; }
    public DateTime Date { get; init; }

    public Transaction(Guid userId, TransactionType type, decimal amount, string category, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Type = type;
        Amount = amount;
        Category = category;
        Date = date;
    }
}
