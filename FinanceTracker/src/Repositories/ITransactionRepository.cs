using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public interface ITransactionRepository
{
    Transaction Add(Transaction transaction);
    IReadOnlyList<Transaction> GetByUser(Guid userId);
    IReadOnlyList<Transaction> GetByUserFiltered(Guid userId, string? category, DateTime? from, DateTime? to);
    Transaction? FindById(Guid id);
    bool Delete(Guid id);
}
