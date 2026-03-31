using FinanceTracker.Console.ApiClients;

namespace FinanceTracker.Console.Menus;

public class ReportMenu
{
    private readonly ReportApiClient _reportClient;

    public ReportMenu(ReportApiClient reportClient) => _reportClient = reportClient;

    public async Task RunAsync(Guid userId)
    {
        ConsoleHelper.PrintHeader("Monthly Summary Report");
        var month = ConsoleHelper.PromptInt("Month", 1, 12);
        var year  = ConsoleHelper.PromptInt("Year", 2000, 2100);

        var summary = await _reportClient.GetMonthlySummaryAsync(userId, month, year);
        if (summary is null)
        {
            ConsoleHelper.PrintError("Could not retrieve summary.");
            ConsoleHelper.WaitForKey();
            return;
        }

        System.Console.WriteLine();
        System.Console.WriteLine($"  Report for {month:D2}/{year}");
        System.Console.WriteLine();
        System.Console.WriteLine($"  Total Income  : {summary.TotalIncome,12:F2}");
        System.Console.WriteLine($"  Total Expense : {summary.TotalExpense,12:F2}");
        System.Console.WriteLine($"  Savings       : {summary.Savings,12:F2}");

        ConsoleHelper.WaitForKey();
    }
}
