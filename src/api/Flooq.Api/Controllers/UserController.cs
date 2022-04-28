using Flooq.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult<IdentityUser>> GetUserById(string? id)
  {
    var actionResult = await _userService.GetUserById(id);

    return actionResult.Value == null ? NotFound() : actionResult;
  }
  
  [HttpGet("/api/User/email/{email}")]
  public async Task<ActionResult<IdentityUser>> GetUserByEmail(string? email)
  {
    var actionResult = await _userService.GetUserByEmail(email);
    return actionResult.Value == null ? NotFound() : actionResult;
  }
  
  [HttpPost]
  public async Task<ActionResult<IdentityUser>> PostUser(IdentityUser user)
  {
    if (UserExists(user.Id))
    {
      return BadRequest();
    }

    _userService.AddUser(user);
    await _userService.SaveChangesAsync();

    return CreatedAtAction("PostUser", new {id = user.Id}, user);
  }

  [HttpPost("/api/User/token/{token}")]
  public async Task<ActionResult<IdentityUserToken<string>>> PostUserToken(IdentityUserToken<string> token)
  {
    if (UserTokenExists(token))
    {
      return BadRequest();
    }

    _userService.AddUserToken(token);
    await _userService.SaveChangesAsync();
    
    return CreatedAtAction("PostUserToken", new {name = token.Name}, token);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(string? id)
  {
    var actionResult = await _userService.GetUserById(id);
    var user = actionResult?.Value;

    if (user == null)
    {
      return NotFound();
    }

    _userService.RemoveUser(user);
    await _userService.SaveChangesAsync();

    return NoContent();
  }

  private bool UserExists(string? id)
  {
    return _userService.UserExists(id);
  }

  private bool UserTokenExists(IdentityUserToken<string> token)
  {
    return _userService.UserTokenExists(token);
  }
}