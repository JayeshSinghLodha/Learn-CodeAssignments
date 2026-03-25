namespace BankingSystem.Domain.Entities;

public class LoanPayment
{
    public int LoanId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public LoanPayment(int loanId, decimal amount, DateTime paymentDate)
    {
        LoanId = loanId;
        Amount = amount;
        PaymentDate = paymentDate;
    }
}
