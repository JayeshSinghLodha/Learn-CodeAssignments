using BankingSystem.Services.Interfaces;

namespace BankingSystem.Managers;

public class TransactionManager
{
    private readonly ITransactionService _transactionService;

    public TransactionManager(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public void Deposit(string accountNumber, decimal amount, string description = "Deposit")
    {
        _transactionService.Deposit(accountNumber, amount, description);
        Console.WriteLine($"Successfully deposited ${amount:N2} to account {accountNumber}");
    }

    public void Withdraw(string accountNumber, decimal amount, string description = "Withdrawal")
    {
        _transactionService.Withdraw(accountNumber, amount, description);
        Console.WriteLine($"Successfully withdrew ${amount:N2} from account {accountNumber}");
    }

    public void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, string description = "Transfer")
    {
        _transactionService.Transfer(fromAccountNumber, toAccountNumber, amount, description);
        Console.WriteLine($"Successfully transferred ${amount:N2} from {fromAccountNumber} to {toAccountNumber}");
    }
}
