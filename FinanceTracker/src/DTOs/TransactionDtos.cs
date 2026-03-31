using FinanceTracker.Models;

namespace FinanceTracker.DTOs;

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
