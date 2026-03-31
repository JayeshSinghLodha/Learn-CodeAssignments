using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public class InMemoryBudgetRepository : IBudgetRepository
{
    private readonly List<Budget> _store = new();

    public Budget Add(Budget budget)
    {
        _store.Add(budget);
        return budget;
    }

    public Budget? FindByUserCategoryAndPeriod(Guid userId, string category, int month, int year) =>
        _store.FirstOrDefault(b =>
            b.UserId == userId &&
            b.Category.Equals(category, StringComparison.OrdinalIgnoreCase) &&
            b.Month == month &&
            b.Year == year);

    public IReadOnlyList<Budget> GetByUser(Guid userId) =>
        _store.Where(b => b.UserId == userId).ToList();
}
