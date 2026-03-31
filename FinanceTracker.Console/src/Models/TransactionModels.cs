namespace FinanceTracker.Console.Models;

public enum TransactionType { Income, Expense }

public record CreateTransactionRequest(
    Guid UserId,
    TransactionType Type,
    decimal Amount,
    string Category,
    DateTime Date);

public record TransactionResponse(
    Guid Id,
    Guid UserId,
    TransactionType Type,
    decimal Amount,
    string Category,
    DateTime Date);
