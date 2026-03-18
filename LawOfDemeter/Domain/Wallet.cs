namespace LawOfDemeterAssignment.Domain;

internal sealed class Wallet
{
    private decimal _value;

    public Wallet(decimal initialAmount)
    {
        if (initialAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialAmount), "Initial amount cannot be negative.");
        }

        _value = initialAmount;
    }

    public bool Pay(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be greater than zero.");
        }

        if (_value < amount)
        {
            return false;
        }

        _value -= amount;
        return true;
    }
}
