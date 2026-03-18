namespace LawOfDemeterAssignment.Domain;

public sealed class Customer
{
    private readonly Wallet _wallet;

    public Customer(string firstName, string lastName, decimal initialWalletBalance)
    {
        FirstName = firstName;
        LastName = lastName;
        _wallet = new Wallet(initialWalletBalance);
    }

    public string FirstName { get; }

    public string LastName { get; }

    public bool Pay(decimal paymentAmount)
    {
        return _wallet.Pay(paymentAmount);
    }
}
