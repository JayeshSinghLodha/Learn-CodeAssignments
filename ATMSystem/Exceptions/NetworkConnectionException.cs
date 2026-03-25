using System;

namespace ATMSystem;

public sealed class NetworkConnectionException : Exception
{
    public NetworkConnectionException(string message) : base(message)
    {
    }
}
