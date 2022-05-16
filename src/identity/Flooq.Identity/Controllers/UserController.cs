using System.Security.Claims;
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

    [HttpPut("{userId}")]
    [Authorize("write")]
    public async Task<ActionResult<IdentityResult>> PutUser(string userId, ApplicationUser user)
    {
      if (userId == null || user == null || GetCurrentUserId() != userId)
      {
        return BadRequest();
      }

      var userToChange = await _userService.GetUsersById(userId);
      return await _userService.SetUserName(userToChange, user.UserName);
    }

    [HttpDelete("{userId}")]
    [Authorize("write")]
    public async Task<ActionResult<IdentityResult>> DeleteUser(string userId)
    {
      if (userId == null || GetCurrentUserId() != userId)
      {
        return BadRequest();
      }

      var user = await _userService.GetUsersById(userId);
      return await _userService.DeleteUser(user);
    }

    private string GetCurrentUserId()
    {
      return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
    }
  }
}