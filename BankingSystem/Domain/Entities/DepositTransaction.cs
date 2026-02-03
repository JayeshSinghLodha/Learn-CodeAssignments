using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class DepositTransaction : Transaction
{
    public DepositTransaction(string accountNumber, decimal amount, string description = "Deposit")
        : base(accountNumber, amount, TransactionType.Deposit, description)
    {
    }

    public override string GetTransactionDetails()
    {
        return $"[DEPOSIT] Amount: ${Amount:N2} | Date: {TransactionDate:g} | {Description}";
    }
}
