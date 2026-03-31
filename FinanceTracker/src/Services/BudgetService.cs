using FinanceTracker.Adapters;
using FinanceTracker.DTOs;
using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services;

public class BudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationAdapter _notificationAdapter;

    public BudgetService(
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository,
        IUserRepository userRepository,
        INotificationAdapter notificationAdapter)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _notificationAdapter = notificationAdapter;
    }

    public BudgetResponse SetBudget(CreateBudgetRequest request)
    {
        var budget = new Budget(request.UserId, request.Category, request.MonthlyLimit, request.Month, request.Year);
        _budgetRepository.Add(budget);
        return ToResponse(budget);
    }

    public IReadOnlyList<BudgetStatusResponse> GetBudgetStatus(Guid userId)
    {
        var budgets = _budgetRepository.GetByUser(userId);
        var user = _userRepository.FindById(userId);

        return budgets.Select(budget =>
        {
            var spent = CalculateSpentForBudget(userId, budget);
            var isExceeded = spent > budget.MonthlyLimit;

            if (isExceeded && user is not null)
                _notificationAdapter.SendBudgetExceededAlert(user.Email, budget.Category, budget.MonthlyLimit, spent);

            return new BudgetStatusResponse(budget.Id, budget.Category, budget.MonthlyLimit, spent, isExceeded);
        }).ToList();
    }

    private decimal CalculateSpentForBudget(Guid userId, Budget budget)
    {
        var periodStart = new DateTime(budget.Year, budget.Month, 1);
        var periodEnd = periodStart.AddMonths(1).AddTicks(-1);

        return _transactionRepository
            .GetByUserFiltered(userId, budget.Category, periodStart, periodEnd)
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);
    }

    private static BudgetResponse ToResponse(Budget budget) =>
        new(budget.Id, budget.UserId, budget.Category, budget.MonthlyLimit, budget.Month, budget.Year);
}
