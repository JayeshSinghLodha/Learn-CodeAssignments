using FinanceTracker.Console.ApiClients;

namespace FinanceTracker.Console.Menus;

public class MainMenu
{
    private readonly UserMenu        _userMenu;
    private readonly TransactionMenu _transactionMenu;
    private readonly BudgetMenu      _budgetMenu;
    private readonly ReportMenu      _reportMenu;

    private Guid? _activeUserId;

    public MainMenu(
        UserMenu        userMenu,
        TransactionMenu transactionMenu,
        BudgetMenu      budgetMenu,
        ReportMenu      reportMenu)
    {
        _userMenu        = userMenu;
        _transactionMenu = transactionMenu;
        _budgetMenu      = budgetMenu;
        _reportMenu      = reportMenu;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            PrintMainMenu();

            switch (System.Console.ReadLine()?.Trim())
            {
                case "1":
                    var selectedId = await _userMenu.RunAsync();
                    if (selectedId.HasValue)
                        _activeUserId = selectedId;
                    break;

                case "2":
                    if (!EnsureActiveUser()) break;
                    await _transactionMenu.RunAsync(_activeUserId!.Value);
                    break;

                case "3":
                    if (!EnsureActiveUser()) break;
                    await _budgetMenu.RunAsync(_activeUserId!.Value);
                    break;

                case "4":
                    if (!EnsureActiveUser()) break;
                    await _reportMenu.RunAsync(_activeUserId!.Value);
                    break;

                case "0":
                    System.Console.WriteLine("\n  Goodbye!");
                    return;

                default:
                    ConsoleHelper.PrintError("Invalid option.");
                    break;
            }
        }
    }

    private void PrintMainMenu()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("  Personal Finance Tracker");
        System.Console.WriteLine("  ------------------------");

        if (_activeUserId.HasValue)
            System.Console.WriteLine($"  Active user: {_activeUserId}");

        System.Console.WriteLine();
        System.Console.WriteLine("  1. Users");
        System.Console.WriteLine("  2. Transactions");
        System.Console.WriteLine("  3. Budgets");
        System.Console.WriteLine("  4. Reports");
        System.Console.WriteLine("  0. Exit");
        System.Console.Write("\n  Choice: ");
    }

    private bool EnsureActiveUser()
    {
        if (_activeUserId.HasValue) return true;

        ConsoleHelper.PrintError("No active user. Please create or look up a user first (option 1).");
        ConsoleHelper.WaitForKey();
        return false;
    }
}
