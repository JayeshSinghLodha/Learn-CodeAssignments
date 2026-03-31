using FinanceTracker.Console.ApiClients;
using FinanceTracker.Console.Models;

namespace FinanceTracker.Console.Menus;

public class TransactionMenu
{
    private readonly TransactionApiClient _transactionClient;

    public TransactionMenu(TransactionApiClient transactionClient) => _transactionClient = transactionClient;

    public async Task RunAsync(Guid userId)
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Transactions");
            System.Console.WriteLine("  1. Add transaction");
            System.Console.WriteLine("  2. View transactions");
            System.Console.WriteLine("  3. Delete transaction");
            System.Console.WriteLine("  0. Back");
            System.Console.Write("\n  Choice: ");

            switch (System.Console.ReadLine()?.Trim())
            {
                case "1": await AddTransactionAsync(userId);   break;
                case "2": await ViewTransactionsAsync(userId); break;
                case "3": await DeleteTransactionAsync();      break;
                case "0": return;
                default: ConsoleHelper.PrintError("Invalid option."); break;
            }
        }
    }

    private async Task AddTransactionAsync(Guid userId)
    {
        ConsoleHelper.PrintHeader("Add Transaction");
        System.Console.WriteLine("  Type: 1 = Income, 2 = Expense");
        var typeChoice = ConsoleHelper.PromptInt("Type", 1, 2);
        var type      = typeChoice == 1 ? TransactionType.Income : TransactionType.Expense;
        var amount    = ConsoleHelper.PromptDecimal("Amount");
        var category  = ConsoleHelper.PromptRequired("Category");
        var date      = ConsoleHelper.PromptDate("Date");

        try
        {
            var tx = await _transactionClient.AddTransactionAsync(new(userId, type, amount, category, date));
            ConsoleHelper.PrintSuccess($"Transaction added. ID: {tx!.Id}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed: {ex.Message}");
        }

        ConsoleHelper.WaitForKey();
    }

    private async Task ViewTransactionsAsync(Guid userId)
    {
        ConsoleHelper.PrintHeader("View Transactions");
        var category = ConsoleHelper.PromptOptional("Filter by category");
        var fromRaw  = ConsoleHelper.PromptOptional("From date (yyyy-MM-dd)");
        var toRaw    = ConsoleHelper.PromptOptional("To date (yyyy-MM-dd)");

        DateTime? from = DateTime.TryParse(fromRaw, out var f) ? f : null;
        DateTime? to   = DateTime.TryParse(toRaw,   out var t) ? t : null;

        var transactions = await _transactionClient.GetTransactionsAsync(
            userId,
            string.IsNullOrWhiteSpace(category) ? null : category,
            from, to);

        System.Console.WriteLine();

        if (!transactions.Any())
        {
            ConsoleHelper.PrintInfo("No transactions found.");
        }
        else
        {
            System.Console.WriteLine($"  {"ID",-38}  {"Type",-8}  {"Amount",10}  {"Category",-15}  {"Date",-12}");
            System.Console.WriteLine($"  {new string('-', 92)}");

            foreach (var tx in transactions)
                System.Console.WriteLine(
                    $"  {tx.Id,-38}  {tx.Type,-8}  {tx.Amount,10:F2}  {tx.Category,-15}  {tx.Date:yyyy-MM-dd}");
        }

        ConsoleHelper.WaitForKey();
    }

    private async Task DeleteTransactionAsync()
    {
        ConsoleHelper.PrintHeader("Delete Transaction");
        var id = ConsoleHelper.PromptGuid("Transaction ID");

        var deleted = await _transactionClient.DeleteTransactionAsync(id);
        if (deleted) ConsoleHelper.PrintSuccess("Transaction deleted.");
        else         ConsoleHelper.PrintError("Transaction not found.");

        ConsoleHelper.WaitForKey();
    }
}
