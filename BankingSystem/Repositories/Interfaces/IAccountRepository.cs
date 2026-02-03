using BankingSystem.Domain.Entities;

namespace BankingSystem.Repositories.Interfaces;

public interface IAccountRepository
{
    Account GetByAccountNumber(string accountNumber);
    IEnumerable<Account> GetByCustomerId(int customerId);
    IEnumerable<Account> GetAll();
    void Add(Account account);
    void Update(Account account);
    bool Exists(string accountNumber);
}
