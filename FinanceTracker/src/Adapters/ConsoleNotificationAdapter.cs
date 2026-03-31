namespace FinanceTracker.Adapters;

/// <summary>
/// Console-based notification adapter — simulates sending alerts
/// without coupling business logic to any real notification system.
/// </summary>
public class ConsoleNotificationAdapter : INotificationAdapter
{
    public void SendBudgetExceededAlert(string userEmail, string category, decimal limit, decimal spent)
    {
        var overspend = spent - limit;
        Console.WriteLine(
            $"[ALERT] Budget exceeded for {userEmail} | " +
            $"Category: {category} | Limit: {limit:C} | Spent: {spent:C} | Over by: {overspend:C}");
    }
}
