using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YuGiOhDeckApi.Data;
using YuGiOhDeckApi.Models;

namespace YuGiOhDeckApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User registrationUser)
    {
        // Basic validation
        if (string.IsNullOrEmpty(registrationUser.Email) || string.IsNullOrEmpty(registrationUser.PasswordHash))
        {
            return BadRequest("Email and password are required.");
        }

        // In a real app, check for existing users first

        await _userService.CreateAsync(registrationUser);
        return Ok(registrationUser);
    }
}
