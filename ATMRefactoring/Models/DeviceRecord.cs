using ATMRefactoring.Enums;

namespace ATMRefactoring.Models;

public class DeviceRecord
{
    public DeviceStatus Status { get; init; }
    public WifiStatus WifiConnection { get; init; }
}
