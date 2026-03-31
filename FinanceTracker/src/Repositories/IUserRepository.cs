using FinanceTracker.Models;

namespace FinanceTracker.Repositories;

public interface IUserRepository
{
    User Add(User user);
    User? FindById(Guid id);
    IReadOnlyList<User> GetAll();
}
