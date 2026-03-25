using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer RegisterCustomer(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        ValidateRegistrationData(firstName, lastName, email, phoneNumber);

        var customer = new Customer(firstName, lastName, email, phoneNumber, dateOfBirth);
        _customerRepository.Add(customer);

        return customer;
    }

    public Customer GetCustomer(int customerId)
    {
        return _customerRepository.GetById(customerId)
            ?? throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _customerRepository.GetAll();
    }

    public void UpdateCustomer(Customer customer)
    {
        if (!_customerRepository.Exists(customer.CustomerId))
            throw new KeyNotFoundException($"Customer with ID {customer.CustomerId} not found.");

        _customerRepository.Update(customer);
    }

    private static void ValidateRegistrationData(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));
    }
}
