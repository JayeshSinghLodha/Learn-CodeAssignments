using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Repositories.InMemory;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts;
    private int _nextAccountNumber;

    public InMemoryAccountRepository()
    {
        _accounts = new List<Account>();
        _nextAccountNumber = 1000;
    }

    public void Add(Account account)
    {
        if (string.IsNullOrEmpty(account.AccountNumber))
        {
            account.AccountNumber = GenerateAccountNumber();
        }
        _accounts.Add(account);
    }

    public Account GetByAccountNumber(string accountNumber)
    {
        return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    public IEnumerable<Account> GetByCustomerId(int customerId)
    {
        return _accounts.Where(a => a.CustomerId == customerId).ToList();
    }

    public IEnumerable<Account> GetAll()
    {
        return _accounts.AsReadOnly();
    }

    public void Update(Account account)
    {
        var existing = GetByAccountNumber(account.AccountNumber);
        if (existing != null)
        {
            var index = _accounts.IndexOf(existing);
            _accounts[index] = account;
        }
    }

    public bool Exists(string accountNumber)
    {
        return _accounts.Any(a => a.AccountNumber == accountNumber);
    }

    private string GenerateAccountNumber()
    {
        return $"ACC{_nextAccountNumber++:D6}";
    }
}
