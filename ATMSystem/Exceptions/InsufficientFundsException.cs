using System;

namespace ATMSystem;

public sealed class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message)
    {
    }
}
