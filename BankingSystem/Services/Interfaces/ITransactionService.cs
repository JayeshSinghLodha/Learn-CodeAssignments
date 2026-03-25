namespace BankingSystem.Services.Interfaces;

public interface ITransactionService
{
    void Deposit(string accountNumber, decimal amount, string description = "Deposit");
    void Withdraw(string accountNumber, decimal amount, string description = "Withdrawal");
    void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, string description = "Transfer");
}
