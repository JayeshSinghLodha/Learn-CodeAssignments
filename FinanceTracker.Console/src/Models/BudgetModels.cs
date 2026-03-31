namespace FinanceTracker.Console.Models;

public record CreateBudgetRequest(
    Guid UserId,
    string Category,
    decimal MonthlyLimit,
    int Month,
    int Year);

public record BudgetResponse(
    Guid Id,
    Guid UserId,
    string Category,
    decimal MonthlyLimit,
    int Month,
    int Year);

public record BudgetStatusResponse(
    Guid BudgetId,
    string Category,
    decimal MonthlyLimit,
    decimal Spent,
    bool IsExceeded);
