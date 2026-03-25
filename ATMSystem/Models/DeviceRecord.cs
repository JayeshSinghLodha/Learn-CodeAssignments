namespace ATMSystem;

public sealed class DeviceRecord
{
    public DeviceStatus Status { get; init; }

    public WifiConnection WifiConnection { get; init; }
}
