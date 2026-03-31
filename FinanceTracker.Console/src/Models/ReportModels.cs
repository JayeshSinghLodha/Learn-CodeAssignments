namespace FinanceTracker.Console.Models;

public record MonthlySummaryResponse(
    int Month,
    int Year,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal Savings);
