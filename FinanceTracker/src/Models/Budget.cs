namespace FinanceTracker.Models;

public class Budget
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Category { get; init; }
    public decimal MonthlyLimit { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }

    public Budget(Guid userId, string category, decimal monthlyLimit, int month, int year)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Category = category;
        MonthlyLimit = monthlyLimit;
        Month = month;
        Year = year;
    }
}
