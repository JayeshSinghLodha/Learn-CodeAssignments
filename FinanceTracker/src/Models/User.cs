namespace FinanceTracker.Models;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public DateTime CreatedAt { get; init; }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
}
