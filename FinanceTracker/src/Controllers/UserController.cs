using FinanceTracker.DTOs;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var user = _userService.CreateUser(request);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _userService.GetUser(id);
        return user is null ? NotFound() : Ok(user);
    }
}
