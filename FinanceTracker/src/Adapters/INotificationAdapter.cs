namespace FinanceTracker.Adapters;

public interface INotificationAdapter
{
    void SendBudgetExceededAlert(string userEmail, string category, decimal limit, decimal spent);
}
