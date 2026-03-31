using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _store = new();

    public Transaction Add(Transaction transaction)
    {
        _store.Add(transaction);
        return transaction;
    }

    public IReadOnlyList<Transaction> GetByUser(Guid userId) =>
        _store.Where(t => t.UserId == userId).ToList();

    public IReadOnlyList<Transaction> GetByUserFiltered(Guid userId, string? category, DateTime? from, DateTime? to)
    {
        var query = _store.Where(t => t.UserId == userId);

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        if (from.HasValue)
            query = query.Where(t => t.Date >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.Date <= to.Value);

        return query.ToList();
    }

    public Transaction? FindById(Guid id) =>
        _store.FirstOrDefault(t => t.Id == id);

    public bool Delete(Guid id)
    {
        var transaction = FindById(id);
        if (transaction is null) return false;

        _store.Remove(transaction);
        return true;
    }
}
