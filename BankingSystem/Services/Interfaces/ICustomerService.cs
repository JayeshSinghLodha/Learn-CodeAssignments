using BankingSystem.Domain.Entities;

namespace BankingSystem.Services.Interfaces;

public interface ICustomerService
{
    Customer RegisterCustomer(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth);
    Customer GetCustomer(int customerId);
    IEnumerable<Customer> GetAllCustomers();
    void UpdateCustomer(Customer customer);
}
