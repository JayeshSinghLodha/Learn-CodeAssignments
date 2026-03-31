namespace FinanceTracker.Console.Models;

public record CreateUserRequest(string Name, string Email);

public record UserResponse(Guid Id, string Name, string Email, DateTime CreatedAt);
