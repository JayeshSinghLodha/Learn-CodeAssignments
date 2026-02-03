using BankingSystem.Domain.Entities;

namespace BankingSystem.Services.Interfaces;

public interface IAccountService
{
    Account CreateAccount(int customerId, decimal initialBalance = 0);
    Account GetAccount(string accountNumber);
    IEnumerable<Account> GetCustomerAccounts(int customerId);
    decimal GetBalance(string accountNumber);
}
