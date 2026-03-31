using FinanceTracker.DTOs;
using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    public TransactionResponse AddTransaction(CreateTransactionRequest request)
    {
        EnsureUserExists(request.UserId);

        var transaction = new Transaction(request.UserId, request.Type, request.Amount, request.Category, request.Date);
        _transactionRepository.Add(transaction);
        return ToResponse(transaction);
    }

    public IReadOnlyList<TransactionResponse> GetTransactions(Guid userId, string? category, DateTime? from, DateTime? to)
    {
        EnsureUserExists(userId);
        return _transactionRepository
            .GetByUserFiltered(userId, category, from, to)
            .Select(ToResponse)
            .ToList();
    }

    public bool DeleteTransaction(Guid id) =>
        _transactionRepository.Delete(id);

    private void EnsureUserExists(Guid userId)
    {
        if (_userRepository.FindById(userId) is null)
            throw new KeyNotFoundException($"User {userId} not found.");
    }

    private static TransactionResponse ToResponse(Transaction t) =>
        new(t.Id, t.UserId, t.Type, t.Amount, t.Category, t.Date);
}
