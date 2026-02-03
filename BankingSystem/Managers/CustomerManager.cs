using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Managers;

public class CustomerManager
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer CreateCustomer(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        ValidateCustomerData(firstName, lastName, email, phoneNumber);

        var customer = new Customer(firstName, lastName, email, phoneNumber, dateOfBirth);
        _customerRepository.Add(customer);

        return customer;
    }

    public Customer GetCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new ArgumentException($"Customer with ID {customerId} not found.");

        return customer;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _customerRepository.GetAll();
    }

    public void UpdateCustomer(Customer customer)
    {
        if (!_customerRepository.Exists(customer.CustomerId))
            throw new ArgumentException($"Customer with ID {customer.CustomerId} does not exist.");

        _customerRepository.Update(customer);
    }

    private void ValidateCustomerData(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");
    }
}
