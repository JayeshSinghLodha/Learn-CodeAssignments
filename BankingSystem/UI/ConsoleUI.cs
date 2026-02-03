using BankingSystem.Managers;

namespace BankingSystem.UI;

public class ConsoleUI
{
    private readonly CustomerManager _customerManager;
    private readonly AccountManager _accountManager;
    private readonly TransactionManager _transactionManager;
    private readonly LoanManager _loanManager;

    public ConsoleUI(
        CustomerManager customerManager,
        AccountManager accountManager,
        TransactionManager transactionManager,
        LoanManager loanManager)
    {
        _customerManager = customerManager;
        _accountManager = accountManager;
        _transactionManager = transactionManager;
        _loanManager = loanManager;
    }

    public void Start()
    {
        Console.WriteLine("\nWELCOME TO BANKING SYSTEM\n");

        bool running = true;
        while (running)
        {
            try
            {
                DisplayMainMenu();
                var choice = GetUserInput("\nEnter your choice: ");

                switch (choice)
                {
                    case "1":
                        CustomerManagementMenu();
                        break;
                    case "2":
                        AccountManagementMenu();
                        break;
                    case "3":
                        TransactionMenu();
                        break;
                    case "4":
                        LoanManagementMenu();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("\nThank you for using Banking System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private void DisplayMainMenu()
    {
        Console.WriteLine("\nMAIN MENU");
        Console.WriteLine("----------");
        Console.WriteLine("1. Customer Management");
        Console.WriteLine("2. Account Management");
        Console.WriteLine("3. Transactions");
        Console.WriteLine("4. Loan Management");
        Console.WriteLine("5. Exit");
    }

    private void CustomerManagementMenu()
    {
        Console.Clear();
        Console.WriteLine("\nCUSTOMER MANAGEMENT");
        Console.WriteLine("-------------------");
        Console.WriteLine("1. Create New Customer");
        Console.WriteLine("2. View Customer Details");
        Console.WriteLine("3. View All Customers");
        Console.WriteLine("4. Back to Main Menu");

        var choice = GetUserInput("\nEnter your choice: ");

        switch (choice)
        {
            case "1":
                CreateCustomer();
                break;
            case "2":
                ViewCustomerDetails();
                break;
            case "3":
                ViewAllCustomers();
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void CreateCustomer()
    {
        Console.WriteLine("\nCreate New Customer");
        Console.WriteLine("-------------------");
        
        var firstName = GetUserInput("First Name: ");
        var lastName = GetUserInput("Last Name: ");
        var email = GetUserInput("Email: ");
        var phoneNumber = GetUserInput("Phone Number: ");
        
        Console.Write("Date of Birth (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        var customer = _customerManager.CreateCustomer(firstName, lastName, email, phoneNumber, dob);
        
        Console.WriteLine("\nCustomer created successfully!");
        Console.WriteLine($"Customer ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.GetFullName()}");
    }

    private void ViewCustomerDetails()
    {
        var customerId = GetIntInput("Enter Customer ID: ");
        var customer = _customerManager.GetCustomer(customerId);

        Console.WriteLine("\nCustomer Details");
        Console.WriteLine("----------------");
        Console.WriteLine($"Customer ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.GetFullName()}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        Console.WriteLine($"Date of Birth: {customer.DateOfBirth:d}");
        Console.WriteLine($"Member Since: {customer.CreatedDate:d}");
    }

    private void ViewAllCustomers()
    {
        var customers = _customerManager.GetAllCustomers().ToList();

        if (!customers.Any())
        {
            Console.WriteLine("\nNo customers found.");
            return;
        }

        Console.WriteLine("\nAll Customers");
        Console.WriteLine("-------------");
        Console.WriteLine($"{"ID",-6} {"Name",-25} {"Email",-30} {"Phone",-15}");
        Console.WriteLine(new string('-', 80));

        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.CustomerId,-6} {customer.GetFullName(),-25} {customer.Email,-30} {customer.PhoneNumber,-15}");
        }

        Console.WriteLine($"\nTotal Customers: {customers.Count}");
    }

    private void AccountManagementMenu()
    {
        Console.Clear();
        Console.WriteLine("\nACCOUNT MANAGEMENT");
        Console.WriteLine("------------------");
        Console.WriteLine("1. Create New Account");
        Console.WriteLine("2. View Account Details");
        Console.WriteLine("3. View Customer Accounts");
        Console.WriteLine("4. Check Balance");
        Console.WriteLine("5. View Transaction History");
        Console.WriteLine("6. Back to Main Menu");

        var choice = GetUserInput("\nEnter your choice: ");

        switch (choice)
        {
            case "1":
                CreateAccount();
                break;
            case "2":
                ViewAccountDetails();
                break;
            case "3":
                ViewCustomerAccounts();
                break;
            case "4":
                CheckBalance();
                break;
            case "5":
                ViewTransactionHistory();
                break;
            case "6":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void CreateAccount()
    {
        Console.WriteLine("\nCreate New Account");
        Console.WriteLine("------------------");
        
        var customerId = GetIntInput("Customer ID: ");
        var initialBalance = GetDecimalInput("Initial Balance (press Enter for 0): ", true);

        var account = _accountManager.CreateAccount(customerId, initialBalance);

        Console.WriteLine("\nAccount created successfully!");
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Initial Balance: ${account.Balance:N2}");
    }

    private void ViewAccountDetails()
    {
        var accountNumber = GetUserInput("Enter Account Number: ");
        var account = _accountManager.GetAccount(accountNumber);

        Console.WriteLine("\nAccount Details");
        Console.WriteLine("---------------");
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Customer ID: {account.CustomerId}");
        Console.WriteLine($"Balance: ${account.Balance:N2}");
        Console.WriteLine($"Created Date: {account.CreatedDate:g}");
        Console.WriteLine($"Total Transactions: {account.TransactionHistory.Count}");
    }

    private void ViewCustomerAccounts()
    {
        var customerId = GetIntInput("Enter Customer ID: ");
        var accounts = _accountManager.GetCustomerAccounts(customerId).ToList();

        if (!accounts.Any())
        {
            Console.WriteLine("\nNo accounts found for this customer.");
            return;
        }

        Console.WriteLine($"\nAccounts for Customer {customerId}");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"{"Account Number",-15} {"Balance",-15} {"Created Date",-20}");
        Console.WriteLine(new string('-', 50));

        foreach (var account in accounts)
        {
            Console.WriteLine($"{account.AccountNumber,-15} ${account.Balance,-14:N2} {account.CreatedDate,-20:g}");
        }

        Console.WriteLine($"\nTotal Accounts: {accounts.Count}");
    }

    private void CheckBalance()
    {
        var accountNumber = GetUserInput("Enter Account Number: ");
        var balance = _accountManager.CheckBalance(accountNumber);

        Console.WriteLine($"\nCurrent Balance: ${balance:N2}");
    }

    private void ViewTransactionHistory()
    {
        var accountNumber = GetUserInput("Enter Account Number: ");
        _accountManager.DisplayTransactionHistory(accountNumber);
    }

    private void TransactionMenu()
    {
        Console.Clear();
        Console.WriteLine("\nTRANSACTIONS");
        Console.WriteLine("------------");
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Transfer");
        Console.WriteLine("4. Back to Main Menu");

        var choice = GetUserInput("\nEnter your choice: ");

        switch (choice)
        {
            case "1":
                PerformDeposit();
                break;
            case "2":
                PerformWithdrawal();
                break;
            case "3":
                PerformTransfer();
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void PerformDeposit()
    {
        Console.WriteLine("\nDeposit");
        Console.WriteLine("-------");
        
        var accountNumber = GetUserInput("Account Number: ");
        var amount = GetDecimalInput("Deposit Amount: ");
        var description = GetUserInput("Description (optional, press Enter to skip): ");

        if (string.IsNullOrWhiteSpace(description))
            description = "Deposit";

        _transactionManager.Deposit(accountNumber, amount, description);
    }

    private void PerformWithdrawal()
    {
        Console.WriteLine("\nWithdrawal");
        Console.WriteLine("----------");
        
        var accountNumber = GetUserInput("Account Number: ");
        var amount = GetDecimalInput("Withdrawal Amount: ");
        var description = GetUserInput("Description (optional, press Enter to skip): ");

        if (string.IsNullOrWhiteSpace(description))
            description = "Withdrawal";

        _transactionManager.Withdraw(accountNumber, amount, description);
    }

    private void PerformTransfer()
    {
        Console.WriteLine("\nTransfer");
        Console.WriteLine("--------");
        
        var fromAccount = GetUserInput("From Account Number: ");
        var toAccount = GetUserInput("To Account Number: ");
        var amount = GetDecimalInput("Transfer Amount: ");
        var description = GetUserInput("Description (optional, press Enter to skip): ");

        if (string.IsNullOrWhiteSpace(description))
            description = "Transfer";

        _transactionManager.Transfer(fromAccount, toAccount, amount, description);
    }

    private void LoanManagementMenu()
    {
        Console.Clear();
        Console.WriteLine("\nLOAN MANAGEMENT");
        Console.WriteLine("---------------");
        Console.WriteLine("1. Create New Loan");
        Console.WriteLine("2. View Loan Details");
        Console.WriteLine("3. Make Loan Payment");
        Console.WriteLine("4. View Customer Loans");
        Console.WriteLine("5. Back to Main Menu");

        var choice = GetUserInput("\nEnter your choice: ");

        switch (choice)
        {
            case "1":
                CreateLoan();
                break;
            case "2":
                ViewLoanDetails();
                break;
            case "3":
                MakeLoanPayment();
                break;
            case "4":
                ViewCustomerLoans();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void CreateLoan()
    {
        Console.WriteLine("\nCreate New Loan");
        Console.WriteLine("---------------");
        
        var customerId = GetIntInput("Customer ID: ");
        var principalAmount = GetDecimalInput("Principal Amount: ");
        var interestRate = GetDecimalInput("Interest Rate (%): ");
        var durationInMonths = GetIntInput("Duration (months): ");

        _loanManager.CreateLoan(customerId, principalAmount, interestRate, durationInMonths);
    }

    private void ViewLoanDetails()
    {
        var loanId = GetIntInput("Enter Loan ID: ");
        _loanManager.DisplayLoanDetails(loanId);
    }

    private void MakeLoanPayment()
    {
        Console.WriteLine("\nMake Loan Payment");
        Console.WriteLine("-----------------");
        
        var loanId = GetIntInput("Loan ID: ");
        var amount = GetDecimalInput("Payment Amount: ");

        _loanManager.MakePayment(loanId, amount);
    }

    private void ViewCustomerLoans()
    {
        var customerId = GetIntInput("Enter Customer ID: ");
        var loans = _loanManager.GetCustomerLoans(customerId).ToList();

        if (!loans.Any())
        {
            Console.WriteLine("\nNo loans found for this customer.");
            return;
        }

        Console.WriteLine($"\nLoans for Customer {customerId}");
        Console.WriteLine("-------------------------------");
        Console.WriteLine($"{"Loan ID",-10} {"Principal",-15} {"Rate",-8} {"Duration",-10} {"Balance",-15} {"Status",-10}");
        Console.WriteLine(new string('-', 80));

        foreach (var loan in loans)
        {
            Console.WriteLine($"{loan.LoanId,-10} ${loan.PrincipalAmount,-14:N2} {loan.InterestRate,-7:N2}% {loan.DurationInMonths,-9}mo ${loan.RemainingBalance,-14:N2} {loan.Status,-10}");
        }

        Console.WriteLine($"\nTotal Loans: {loans.Count}");
    }

    private string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private int GetIntInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result))
                return result;

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private decimal GetDecimalInput(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();

            if (allowEmpty && string.IsNullOrWhiteSpace(input))
                return 0;

            if (decimal.TryParse(input, out decimal result))
                return result;

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}
