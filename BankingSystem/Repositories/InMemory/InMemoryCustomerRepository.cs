using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Repositories.InMemory;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers;
    private int _nextId;

    public InMemoryCustomerRepository()
    {
        _customers = new List<Customer>();
        _nextId = 1;
    }

    public void Add(Customer customer)
    {
        customer.CustomerId = _nextId++;
        _customers.Add(customer);
    }

    public Customer GetById(int customerId)
    {
        return _customers.FirstOrDefault(c => c.CustomerId == customerId);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customers.AsReadOnly();
    }

    public void Update(Customer customer)
    {
        var existing = GetById(customer.CustomerId);
        if (existing != null)
        {
            var index = _customers.IndexOf(existing);
            _customers[index] = customer;
        }
    }

    public bool Exists(int customerId)
    {
        return _customers.Any(c => c.CustomerId == customerId);
    }
}
