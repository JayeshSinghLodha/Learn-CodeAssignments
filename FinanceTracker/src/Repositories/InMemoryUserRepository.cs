using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _store = new();

    public User Add(User user)
    {
        _store[user.Id] = user;
        return user;
    }

    public User? FindById(Guid id) =>
        _store.TryGetValue(id, out var user) ? user : null;

    public IReadOnlyList<User> GetAll() =>
        _store.Values.ToList();
}
