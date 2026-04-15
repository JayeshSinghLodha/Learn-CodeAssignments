namespace ATMRefactoring.Models;

public class DeviceHandle
{
    public string DeviceId { get; }
    public bool IsValid { get; }

    private DeviceHandle(string deviceId, bool isValid)
    {
        DeviceId = deviceId;
        IsValid = isValid;
    }

    public static DeviceHandle Create(string deviceId) => new(deviceId, isValid: true);
    public static DeviceHandle Invalid => new(string.Empty, isValid: false);
}
