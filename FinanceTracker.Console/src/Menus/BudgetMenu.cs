using FinanceTracker.Console.ApiClients;

namespace FinanceTracker.Console.Menus;

public class BudgetMenu
{
    private readonly BudgetApiClient _budgetClient;

    public BudgetMenu(BudgetApiClient budgetClient) => _budgetClient = budgetClient;

    public async Task RunAsync(Guid userId)
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Budgets");
            System.Console.WriteLine("  1. Set monthly budget");
            System.Console.WriteLine("  2. View budgets and status");
            System.Console.WriteLine("  0. Back");
            System.Console.Write("\n  Choice: ");

            switch (System.Console.ReadLine()?.Trim())
            {
                case "1": await SetBudgetAsync(userId);   break;
                case "2": await ViewBudgetsAsync(userId); break;
                case "0": return;
                default: ConsoleHelper.PrintError("Invalid option."); break;
            }
        }
    }

    private async Task SetBudgetAsync(Guid userId)
    {
        ConsoleHelper.PrintHeader("Set Monthly Budget");
        var category = ConsoleHelper.PromptRequired("Category");
        var limit    = ConsoleHelper.PromptDecimal("Monthly limit");
        var month    = ConsoleHelper.PromptInt("Month", 1, 12);
        var year     = ConsoleHelper.PromptInt("Year", 2000, 2100);

        try
        {
            var budget = await _budgetClient.SetBudgetAsync(new(userId, category, limit, month, year));
            ConsoleHelper.PrintSuccess($"Budget set. ID: {budget!.Id}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed: {ex.Message}");
        }

        ConsoleHelper.WaitForKey();
    }

    private async Task ViewBudgetsAsync(Guid userId)
    {
        ConsoleHelper.PrintHeader("Budget Status");
        var budgets = await _budgetClient.GetBudgetsAsync(userId);

        System.Console.WriteLine();

        if (!budgets.Any())
        {
            ConsoleHelper.PrintInfo("No budgets found.");
        }
        else
        {
            System.Console.WriteLine($"  {"Category",-15}  {"Limit",10}  {"Spent",10}  {"Status",-10}");
            System.Console.WriteLine($"  {new string('-', 55)}");

            foreach (var b in budgets)
            {
                var status = b.IsExceeded ? "EXCEEDED" : "OK";
                System.Console.WriteLine(
                    $"  {b.Category,-15}  {b.MonthlyLimit,10:F2}  {b.Spent,10:F2}  {status,-10}");
            }
        }

        ConsoleHelper.WaitForKey();
    }
}
