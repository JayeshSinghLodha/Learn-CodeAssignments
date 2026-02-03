using BankingSystem.Domain.Entities;

namespace BankingSystem.Services.Interfaces;

public interface ILoanService
{
    Loan CreateLoan(int customerId, decimal principalAmount, decimal interestRate, int durationInMonths);
    void MakePayment(int loanId, decimal amount);
    Loan GetLoan(int loanId);
    IEnumerable<Loan> GetCustomerLoans(int customerId);
    decimal CalculateTotalInterest(int loanId);
    decimal CalculateMonthlyPayment(int loanId);
}
