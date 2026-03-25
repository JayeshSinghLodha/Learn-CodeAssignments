using System;

namespace ATMSystem;

public class ATMDeviceController
{
    public void Withdraw(string accountId, decimal amount)
    {
        var handle = GetValidHandle();
        var record = GetAvailableDeviceRecord(handle);

        EnsureDeviceIsActive(record);
        EnsureWifiIsConnected(record);
        EnsureSufficientFunds(accountId, amount);

        DispenseCash(handle, amount);
    }

    public string ProcessWithdrawal(string accountId, decimal amount)
    {
        try
        {
            Withdraw(accountId, amount);
            return "Withdrawal completed successfully.";
        }
        catch (DeviceLockedException)
        {
            return "The ATM is currently suspended.";
        }
        catch (NetworkConnectionException)
        {
            return "The ATM is offline.";
        }
        catch (InsufficientFundsException)
        {
            return "The account does not have enough funds.";
        }
    }

    private DeviceHandle GetValidHandle()
    {
        var handle = GetHandle("DEV1");

        if (handle == DeviceHandle.Invalid)
        {
            throw new InvalidOperationException("Unable to access ATM device.");
        }

        return handle;
    }

    private DeviceRecord GetAvailableDeviceRecord(DeviceHandle handle)
    {
        return RetrieveDeviceRecord(handle);
    }

    private static void EnsureDeviceIsActive(DeviceRecord record)
    {
        if (record.Status == DeviceStatus.Suspended)
        {
            throw new DeviceLockedException("The ATM device is suspended.");
        }
    }

    private static void EnsureWifiIsConnected(DeviceRecord record)
    {
        if (record.WifiConnection != WifiConnection.Connected)
        {
            throw new NetworkConnectionException("The ATM device is not connected to the network.");
        }
    }

    private void EnsureSufficientFunds(string accountId, decimal amount)
    {
        if (GetBalance(accountId) < amount)
        {
            throw new InsufficientFundsException("Insufficient funds for this withdrawal.");
        }
    }

    private DeviceHandle GetHandle(string deviceId)
    {
        throw new NotImplementedException();
    }

    private DeviceRecord RetrieveDeviceRecord(DeviceHandle handle)
    {
        throw new NotImplementedException();
    }

    private decimal GetBalance(string accountId)
    {
        throw new NotImplementedException();
    }

    private void DispenseCash(DeviceHandle handle, decimal amount)
    {
        throw new NotImplementedException();
    }
}
