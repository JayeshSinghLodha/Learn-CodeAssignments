using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services.Interfaces;

namespace BankingSystem.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;

    public TransactionService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Deposit(string accountNumber, decimal amount, string description = "Deposit")
    {
        var account = GetAccountOrThrow(accountNumber);

        account.Deposit(amount);
        var transaction = new DepositTransaction(accountNumber, amount, description);
        account.AddTransaction(transaction);

        _accountRepository.Update(account);
    }

    public void Withdraw(string accountNumber, decimal amount, string description = "Withdrawal")
    {
        var account = GetAccountOrThrow(accountNumber);

        account.Withdraw(amount);
        var transaction = new WithdrawalTransaction(accountNumber, amount, description);
        account.AddTransaction(transaction);

        _accountRepository.Update(account);
    }

    public void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, string description = "Transfer")
    {
        if (fromAccountNumber == toAccountNumber)
            throw new ArgumentException("Cannot transfer to the same account.");

        var fromAccount = GetAccountOrThrow(fromAccountNumber);
        var toAccount = GetAccountOrThrow(toAccountNumber);

        fromAccount.Withdraw(amount);
        toAccount.Deposit(amount);

        var transferOut = new TransferTransaction(
            fromAccountNumber, 
            toAccountNumber, 
            amount, 
            $"{description} (Out)");
        
        var transferIn = new TransferTransaction(
            toAccountNumber, 
            fromAccountNumber, 
            amount, 
            $"{description} (In)");

        fromAccount.AddTransaction(transferOut);
        toAccount.AddTransaction(transferIn);

        _accountRepository.Update(fromAccount);
        _accountRepository.Update(toAccount);
    }

    private Account GetAccountOrThrow(string accountNumber)
    {
        var account = _accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new ArgumentException($"Account {accountNumber} not found.");

        return account;
    }
}
