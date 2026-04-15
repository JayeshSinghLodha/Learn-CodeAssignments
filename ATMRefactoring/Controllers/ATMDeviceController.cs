using ATMRefactoring.Enums;
using ATMRefactoring.Exceptions;
using ATMRefactoring.Models;

namespace ATMRefactoring.Controllers;

public class ATMDeviceController
{
    public void Withdraw(string accountId, double amount)
    {
        DeviceHandle handle = GetHandle("DEV1");
        EnsureDeviceIsReachable(handle);

        DeviceRecord record = RetrieveDeviceRecord(handle);
        EnsureDeviceIsOperational(record);

        EnsureSufficientFunds(accountId, amount);

        DispenseCash(handle, amount);
    }

    // --- Guard methods: each throws a specific exception on failure ---

    private static void EnsureDeviceIsReachable(DeviceHandle handle)
    {
        if (!handle.IsValid)
            throw new DeviceLockedException("Device handle is invalid; the ATM cannot be reached.");
    }

    private static void EnsureDeviceIsOperational(DeviceRecord record)
    {
        if (record.Status == DeviceStatus.Suspended)
            throw new DeviceLockedException("The ATM device is currently suspended.");

        if (record.WifiConnection != WifiStatus.Connected)
            throw new NetworkConnectionException("The ATM has no active network connection.");
    }

    private void EnsureSufficientFunds(string accountId, double amount)
    {
        if (GetBalance(accountId) < amount)
            throw new InsufficientFundsException($"Account {accountId} has insufficient funds for withdrawal of {amount:C}.");
    }

    private static DeviceHandle GetHandle(string deviceId) =>
        DeviceHandle.Create(deviceId);

    private static DeviceRecord RetrieveDeviceRecord(DeviceHandle handle) =>
        new() { Status = DeviceStatus.Active, WifiConnection = WifiStatus.Connected };

    private static double GetBalance(string accountId) => 500.00;

    private static void DispenseCash(DeviceHandle handle, double amount) =>
        Console.WriteLine($"Dispensing {amount:C} from device {handle.DeviceId}.");
}
