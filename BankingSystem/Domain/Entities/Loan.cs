using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class Loan
{
    public int LoanId { get; set; }
    public int CustomerId { get; set; }
    public decimal PrincipalAmount { get; private set; }
    public decimal InterestRate { get; set; }
    public int DurationInMonths { get; set; }
    public decimal RemainingBalance { get; private set; }
    public LoanStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<LoanPayment> Payments { get; private set; }

    public Loan(int customerId, decimal principalAmount, decimal interestRate, int durationInMonths)
    {
        CustomerId = customerId;
        PrincipalAmount = principalAmount;
        InterestRate = interestRate;
        DurationInMonths = durationInMonths;
        RemainingBalance = CalculateTotalAmountWithInterest();
        Status = LoanStatus.Active;
        CreatedDate = DateTime.Now;
        Payments = new List<LoanPayment>();
    }

    public decimal CalculateTotalAmountWithInterest()
    {
        decimal timeInYears = DurationInMonths / 12m;
        return PrincipalAmount * (1 + (InterestRate / 100m * timeInYears));
    }

    public decimal CalculateMonthlyPayment()
    {
        return CalculateTotalAmountWithInterest() / DurationInMonths;
    }

    public void MakePayment(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be positive.");

        if (Status != LoanStatus.Active)
            throw new InvalidOperationException("Cannot make payment on inactive loan.");

        if (amount > RemainingBalance)
            amount = RemainingBalance;

        RemainingBalance -= amount;
        Payments.Add(new LoanPayment(LoanId, amount, DateTime.Now));

        if (RemainingBalance == 0)
            Status = LoanStatus.Paid;
    }
}
