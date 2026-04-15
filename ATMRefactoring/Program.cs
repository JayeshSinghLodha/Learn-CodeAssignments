using ATMRefactoring.Controllers;
using ATMRefactoring.Exceptions;

ATMDeviceController atm = new();

try
{
    atm.Withdraw("ACC-001", 200.00);
    Console.WriteLine("Withdrawal successful.");
}
catch (DeviceLockedException ex)
{
    Console.WriteLine($"[Device Error] {ex.Message}");
}
catch (NetworkConnectionException ex)
{
    Console.WriteLine($"[Network Error] {ex.Message}");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"[Funds Error] {ex.Message}");
}
