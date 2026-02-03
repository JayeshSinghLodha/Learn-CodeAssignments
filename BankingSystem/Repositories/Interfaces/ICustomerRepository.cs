using BankingSystem.Domain.Entities;

namespace BankingSystem.Repositories.Interfaces;

public interface ICustomerRepository
{
    Customer GetById(int customerId);
    IEnumerable<Customer> GetAll();
    void Add(Customer customer);
    void Update(Customer customer);
    bool Exists(int customerId);
}
