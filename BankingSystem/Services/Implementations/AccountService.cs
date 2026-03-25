using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;

    public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
    }

    public Account CreateAccount(int customerId, decimal initialBalance = 0)
    {
        if (!_customerRepository.Exists(customerId))
            throw new ArgumentException($"Customer with ID {customerId} does not exist.");

        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");

        var account = new Account(string.Empty, customerId, initialBalance);
        _accountRepository.Add(account);

        return account;
    }

    public Account GetAccount(string accountNumber)
    {
        var account = _accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new ArgumentException($"Account {accountNumber} not found.");

        return account;
    }

    public IEnumerable<Account> GetCustomerAccounts(int customerId)
    {
        return _accountRepository.GetByCustomerId(customerId);
    }

    public decimal GetBalance(string accountNumber)
    {
        var account = GetAccount(accountNumber);
        return account.Balance;
    }
}
