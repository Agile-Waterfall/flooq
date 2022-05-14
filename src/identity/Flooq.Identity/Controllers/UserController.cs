using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Flooq.Identity.Services;
using Flooq.Identity.Models;

namespace Flooq.Identity.Controllers
{
  [Route("api/user")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous] // TODO: Add Authorization
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
    {
      return await _userService.GetUsers();
    }

    [HttpGet("{userId}")]
    [AllowAnonymous] // TODO: Add Authorization
    public async Task<ActionResult<ApplicationUser>> GetUserById(string userId)
    {
      return await _userService.GetUsersById(userId);
    }

    [HttpPut("{userId}")]
    [AllowAnonymous] // TODO: Add Authorization
    public async Task<ActionResult<IdentityResult>> PutUserName(string userId, ApplicationUser user)
    {
      if (userId == null || user == null)
      {
        return BadRequest();
      }

      var userToChange = await _userService.GetUsersById(userId);
      return await _userService.SetUserName(userToChange, user.UserName);
    }

    [HttpDelete("{userId}")]
    [AllowAnonymous] // TODO: Add Authorization
    public async Task<ActionResult<IdentityResult>> DeleteUser(string userId)
    {
      if (userId == null)
      {
        return BadRequest();
      }

      var user = await _userService.GetUsersById(userId);
      return await _userService.DeleteUser(user);
    }
  }
}