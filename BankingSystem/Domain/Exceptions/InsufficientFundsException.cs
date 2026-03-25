namespace BankingSystem.Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    public string AccountNumber { get; }
    public decimal AttemptedAmount { get; }
    public decimal AvailableBalance { get; }

    public InsufficientFundsException(string accountNumber, decimal attemptedAmount, decimal availableBalance)
        : base(
            $"Account {accountNumber} has insufficient funds. " +
            $"Attempted withdrawal: ${attemptedAmount:N2}. " +
            $"Available balance: ${availableBalance:N2}.")
    {
        AccountNumber = accountNumber;
        AttemptedAmount = attemptedAmount;
        AvailableBalance = availableBalance;
    }
}
