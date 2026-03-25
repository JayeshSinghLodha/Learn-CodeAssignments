using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;

    public LoanService(ILoanRepository loanRepository, ICustomerRepository customerRepository)
    {
        _loanRepository = loanRepository;
        _customerRepository = customerRepository;
    }

    public Loan CreateLoan(int customerId, decimal principalAmount, decimal interestRate, int durationInMonths)
    {
        if (!_customerRepository.Exists(customerId))
            throw new ArgumentException($"Customer with ID {customerId} does not exist.");

        if (principalAmount <= 0)
            throw new ArgumentException("Principal amount must be positive.");

        if (interestRate < 0)
            throw new ArgumentException("Interest rate cannot be negative.");

        if (durationInMonths <= 0)
            throw new ArgumentException("Duration must be positive.");

        var loan = new Loan(customerId, principalAmount, interestRate, durationInMonths);
        _loanRepository.Add(loan);

        return loan;
    }

    public void MakePayment(int loanId, decimal amount)
    {
        var loan = GetLoan(loanId);
        loan.MakePayment(amount);
        _loanRepository.Update(loan);
    }

    public Loan GetLoan(int loanId)
    {
        var loan = _loanRepository.GetById(loanId);
        if (loan == null)
            throw new ArgumentException($"Loan with ID {loanId} not found.");

        return loan;
    }

    public IEnumerable<Loan> GetCustomerLoans(int customerId)
    {
        return _loanRepository.GetByCustomerId(customerId);
    }

    public decimal CalculateTotalInterest(int loanId)
    {
        var loan = GetLoan(loanId);
        return loan.CalculateTotalAmountWithInterest() - loan.PrincipalAmount;
    }

    public decimal CalculateMonthlyPayment(int loanId)
    {
        var loan = GetLoan(loanId);
        return loan.CalculateMonthlyPayment();
    }
}
