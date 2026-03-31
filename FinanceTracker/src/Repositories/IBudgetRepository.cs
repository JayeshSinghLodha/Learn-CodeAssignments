using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public interface IBudgetRepository
{
    Budget Add(Budget budget);
    Budget? FindByUserCategoryAndPeriod(Guid userId, string category, int month, int year);
    IReadOnlyList<Budget> GetByUser(Guid userId);
}
