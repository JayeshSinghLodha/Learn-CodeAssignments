using BankingSystem.Domain.Entities;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Managers;

public class LoanManager
{
    private readonly ILoanService _loanService;

    public LoanManager(ILoanService loanService)
    {
        _loanService = loanService;
    }

    public Loan CreateLoan(int customerId, decimal principalAmount, decimal interestRate, int durationInMonths)
    {
        var loan = _loanService.CreateLoan(
            customerId, 
            principalAmount, 
            interestRate, 
            durationInMonths);
        
        Console.WriteLine("\nLoan Created Successfully");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Loan ID: {loan.LoanId}");
        Console.WriteLine($"Principal Amount: ${loan.PrincipalAmount:N2}");
        Console.WriteLine($"Interest Rate: {loan.InterestRate}%");
        Console.WriteLine($"Duration: {loan.DurationInMonths} months");
        Console.WriteLine(
            $"Total Amount (with interest): " +
            $"${loan.CalculateTotalAmountWithInterest():N2}");
        Console.WriteLine($"Monthly Payment: ${loan.CalculateMonthlyPayment():N2}");

        return loan;
    }

    public void MakePayment(int loanId, decimal amount)
    {
        var loan = _loanService.GetLoan(loanId);
        var previousBalance = loan.RemainingBalance;

        _loanService.MakePayment(loanId, amount);

        Console.WriteLine($"\nPayment of ${amount:N2} processed successfully.");
        Console.WriteLine($"Previous Balance: ${previousBalance:N2}");
        Console.WriteLine($"Remaining Balance: ${loan.RemainingBalance:N2}");
        
        if (loan.RemainingBalance == 0)
            Console.WriteLine("Congratulations! Loan has been fully paid.");
    }

    public void DisplayLoanDetails(int loanId)
    {
        var loan = _loanService.GetLoan(loanId);
        var totalInterest = _loanService.CalculateTotalInterest(loanId);

        Console.WriteLine("\nLoan Details");
        Console.WriteLine("------------");
        Console.WriteLine($"Loan ID: {loan.LoanId}");
        Console.WriteLine($"Status: {loan.Status}");
        Console.WriteLine($"Principal Amount: ${loan.PrincipalAmount:N2}");
        Console.WriteLine($"Interest Rate: {loan.InterestRate}%");
        Console.WriteLine($"Duration: {loan.DurationInMonths} months");
        Console.WriteLine($"Total Interest: ${totalInterest:N2}");
        Console.WriteLine($"Total Amount: ${loan.CalculateTotalAmountWithInterest():N2}");
        Console.WriteLine($"Monthly Payment: ${loan.CalculateMonthlyPayment():N2}");
        Console.WriteLine($"Remaining Balance: ${loan.RemainingBalance:N2}");
        Console.WriteLine($"Created Date: {loan.CreatedDate:g}");

        if (loan.Payments.Any())
        {
            Console.WriteLine("\nPayment History");
            Console.WriteLine("---------------");
            foreach (var payment in loan.Payments)
            {
                Console.WriteLine($"  ${payment.Amount:N2} on {payment.PaymentDate:g}");
            }
        }
    }

    public IEnumerable<Loan> GetCustomerLoans(int customerId)
    {
        return _loanService.GetCustomerLoans(customerId);
    }
}
