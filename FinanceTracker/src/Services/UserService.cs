using FinanceTracker.DTOs;
using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserResponse CreateUser(CreateUserRequest request)
    {
        var user = new User(request.Name, request.Email);
        _userRepository.Add(user);
        return ToResponse(user);
    }

    public UserResponse? GetUser(Guid id)
    {
        var user = _userRepository.FindById(id);
        return user is null ? null : ToResponse(user);
    }

    private static UserResponse ToResponse(User user) =>
        new(user.Id, user.Name, user.Email, user.CreatedAt);
}
