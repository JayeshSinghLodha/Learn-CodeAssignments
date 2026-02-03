using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class TransferTransaction : Transaction
{
    public string ToAccountNumber { get; set; }

    public TransferTransaction(string fromAccountNumber, string toAccountNumber, decimal amount, string description = "Transfer")
        : base(fromAccountNumber, amount, TransactionType.Transfer, description)
    {
        ToAccountNumber = toAccountNumber;
    }

    public override string GetTransactionDetails()
    {
        return $"[TRANSFER] Amount: ${Amount:N2} | To: {ToAccountNumber} | Date: {TransactionDate:g} | {Description}";
    }
}
