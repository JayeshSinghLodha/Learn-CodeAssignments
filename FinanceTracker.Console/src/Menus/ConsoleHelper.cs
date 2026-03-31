namespace FinanceTracker.Console.Menus;

internal static class ConsoleHelper
{
    internal static void PrintHeader(string title)
    {
        System.Console.WriteLine();
        System.Console.WriteLine($"  {title}");
        System.Console.WriteLine($"  {new string('-', title.Length)}");
    }

    internal static void PrintSuccess(string message) =>
        System.Console.WriteLine($"  {message}");

    internal static void PrintError(string message) =>
        System.Console.WriteLine($"  Error: {message}");

    internal static void PrintInfo(string message) =>
        System.Console.WriteLine($"  {message}");

    internal static string PromptRequired(string label)
    {
        while (true)
        {
            System.Console.Write($"  {label}: ");
            var value = System.Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(value)) return value;
            PrintError("This field is required.");
        }
    }

    internal static string PromptOptional(string label)
    {
        System.Console.Write($"  {label} (press Enter to skip): ");
        return System.Console.ReadLine()?.Trim() ?? string.Empty;
    }

    internal static decimal PromptDecimal(string label)
    {
        while (true)
        {
            var raw = PromptRequired(label);
            if (decimal.TryParse(raw, out var value) && value > 0) return value;
            PrintError("Enter a valid positive number.");
        }
    }

    internal static int PromptInt(string label, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            var raw = PromptRequired(label);
            if (int.TryParse(raw, out var value) && value >= min && value <= max) return value;
            PrintError($"Enter a number between {min} and {max}.");
        }
    }

    internal static DateTime PromptDate(string label)
    {
        while (true)
        {
            var raw = PromptRequired($"{label} (yyyy-MM-dd)");
            if (DateTime.TryParse(raw, out var value)) return value;
            PrintError("Enter a valid date in yyyy-MM-dd format.");
        }
    }

    internal static Guid PromptGuid(string label)
    {
        while (true)
        {
            var raw = PromptRequired(label);
            if (Guid.TryParse(raw, out var value)) return value;
            PrintError("Enter a valid GUID.");
        }
    }

    internal static void WaitForKey()
    {
        System.Console.WriteLine();
        System.Console.Write("  Press any key to continue...");
        System.Console.ReadKey(true);
        System.Console.WriteLine();
    }
}
