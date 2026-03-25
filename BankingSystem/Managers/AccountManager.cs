using BankingSystem.Domain.Entities;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Managers;

public class AccountManager
{
    private readonly IAccountService _accountService;

    public AccountManager(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Account CreateAccount(int customerId, decimal initialBalance = 0)
    {
        return _accountService.CreateAccount(customerId, initialBalance);
    }

    public Account GetAccount(string accountNumber)
    {
        return _accountService.GetAccount(accountNumber);
    }

    public IEnumerable<Account> GetCustomerAccounts(int customerId)
    {
        return _accountService.GetCustomerAccounts(customerId);
    }

    public decimal CheckBalance(string accountNumber)
    {
        return _accountService.GetBalance(accountNumber);
    }

    public void DisplayTransactionHistory(string accountNumber)
    {
        var account = _accountService.GetAccount(accountNumber);
        
        Console.WriteLine($"\nTransaction History for Account {accountNumber}");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine($"Current Balance: ${account.Balance:N2}\n");

        if (!account.TransactionHistory.Any())
        {
            Console.WriteLine("No transactions found.");
            return;
        }

        foreach (var transaction in account.TransactionHistory.OrderByDescending(t => t.TransactionDate))
        {
            Console.WriteLine(transaction.GetTransactionDetails());
        }
    }
}
