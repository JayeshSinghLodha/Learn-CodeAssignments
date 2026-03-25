using System;

namespace ATMSystem;

public class DeviceLockedException : Exception
{
    public DeviceLockedException(string message) : base(message)
    {
    }
}
