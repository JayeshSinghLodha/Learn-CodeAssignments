using FinanceTracker.Console.ApiClients;

namespace FinanceTracker.Console.Menus;

public class UserMenu
{
    private readonly UserApiClient _userClient;

    public UserMenu(UserApiClient userClient) => _userClient = userClient;

    public async Task<Guid?> RunAsync()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("User Management");
            System.Console.WriteLine("  1. Create new user");
            System.Console.WriteLine("  2. Look up user by ID");
            System.Console.WriteLine("  0. Back");
            System.Console.Write("\n  Choice: ");

            switch (System.Console.ReadLine()?.Trim())
            {
                case "1": return await CreateUserAsync();
                case "2": return await LookupUserAsync();
                case "0": return null;
                default: ConsoleHelper.PrintError("Invalid option."); break;
            }
        }
    }

    private async Task<Guid?> CreateUserAsync()
    {
        ConsoleHelper.PrintHeader("Create User");
        var name  = ConsoleHelper.PromptRequired("Name");
        var email = ConsoleHelper.PromptRequired("Email");

        try
        {
            var user = await _userClient.CreateUserAsync(new(name, email));
            ConsoleHelper.PrintSuccess($"User created. ID: {user!.Id}");
            ConsoleHelper.WaitForKey();
            return user.Id;
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed to create user: {ex.Message}");
            ConsoleHelper.WaitForKey();
            return null;
        }
    }

    private async Task<Guid?> LookupUserAsync()
    {
        ConsoleHelper.PrintHeader("Look Up User");
        var id = ConsoleHelper.PromptGuid("User ID");

        var user = await _userClient.GetUserAsync(id);
        if (user is null)
        {
            ConsoleHelper.PrintError("User not found.");
            ConsoleHelper.WaitForKey();
            return null;
        }

        System.Console.WriteLine();
        System.Console.WriteLine($"  Name  : {user.Name}");
        System.Console.WriteLine($"  Email : {user.Email}");
        System.Console.WriteLine($"  Joined: {user.CreatedAt:yyyy-MM-dd}");
        ConsoleHelper.WaitForKey();
        return user.Id;
    }
}
