using BankingSystem.Managers;
using BankingSystem.Repositories.InMemory;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services.Implementations;
using BankingSystem.Services.Interfaces;
using BankingSystem.UI;

namespace BankingSystem;

class Program
{
    static void Main(string[] args)
    {
        ICustomerRepository customerRepository = new InMemoryCustomerRepository();
        IAccountRepository accountRepository = new InMemoryAccountRepository();
        ILoanRepository loanRepository = new InMemoryLoanRepository();

        IAccountService accountService = new AccountService(accountRepository, customerRepository);
        ITransactionService transactionService = new TransactionService(accountRepository);
        ILoanService loanService = new LoanService(loanRepository, customerRepository);

        var customerManager = new CustomerManager(customerRepository);
        var accountManager = new AccountManager(accountService);
        var transactionManager = new TransactionManager(transactionService);
        var loanManager = new LoanManager(loanService);

        var consoleUI = new ConsoleUI(customerManager, accountManager, transactionManager, loanManager);
        consoleUI.Start();
    }
}
