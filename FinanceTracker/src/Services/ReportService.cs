using FinanceTracker.DTOs;
using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services;

public class ReportService
{
    private readonly ITransactionRepository _transactionRepository;

    public ReportService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public MonthlySummaryResponse GetMonthlySummary(Guid userId, int month, int year)
    {
        var periodStart = new DateTime(year, month, 1);
        var periodEnd = periodStart.AddMonths(1).AddTicks(-1);

        var transactions = _transactionRepository.GetByUserFiltered(userId, null, periodStart, periodEnd);

        var totalIncome = transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var totalExpense = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var savings = totalIncome - totalExpense;

        return new MonthlySummaryResponse(month, year, totalIncome, totalExpense, savings);
    }
}
