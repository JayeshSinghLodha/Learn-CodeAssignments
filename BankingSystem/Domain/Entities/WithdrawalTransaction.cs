using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class WithdrawalTransaction : Transaction
{
    public WithdrawalTransaction(string accountNumber, decimal amount, string description = "Withdrawal")
        : base(accountNumber, amount, TransactionType.Withdrawal, description)
    {
    }

    public override string GetTransactionDetails()
    {
        return $"[WITHDRAWAL] Amount: ${Amount:N2} | Date: {TransactionDate:g} | {Description}";
    }
}
