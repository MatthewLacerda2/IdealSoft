using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase {

    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager) {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto, string password) {

        var existingEmail = await _userManager.FindByEmailAsync(userDto.Email);
        if (existingEmail != null) {
            return BadRequest("Email already registered!");
        }

        var existingUsername = await _userManager.FindByNameAsync(userDto.UserName);
        if (existingUsername != null) {
            return BadRequest("UserName already registered!");
        }
        
        User _user = (User)userDto;

        var result = await _userManager.CreateAsync(_user, password);

        if(!result.Succeeded){
            return StatusCode(500, "Internal Server Error: Register Client Unsuccessful\n\n"+result.Errors);
        }

        return CreatedAtAction(nameof(CreateUser), (UserDto)_user);
    }
}