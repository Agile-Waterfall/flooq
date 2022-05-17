using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Flooq.Identity.Services;
using Flooq.Identity.Models;

namespace Flooq.Identity.Controllers
{
  /// <summary>
  /// Responsible to allow updates to the user accounts.
  /// </summary>
  [Route("api/user")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    /// <summary>
    /// Creates a new instance of the UserController.
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    /// <summary>
    /// Gets the user information for the current user.
    /// </summary>
    /// <returns>The current user</returns>
    [HttpGet]
    [Authorize(Policy = "read")]
    public async Task<ActionResult<ApplicationUser>> GetCurrentUser()
    {
      return await _userService.GetUsersById(GetCurrentUserId());
    }

    /// <summary>
    /// Updates the current user.
    /// </summary>
    /// <param name="userId">Of the user to update</param>
    /// <param name="user">New user object to update the current user with</param>
    /// <returns>The result of the update</returns>
    [HttpPut("{userId}")]
    [Authorize(Policy = "write")]
    public async Task<ActionResult<IdentityResult>> PutUser(string userId, ApplicationUser user)
    {
      if (userId == null || user == null || GetCurrentUserId() != userId)
      {
        return BadRequest();
      }

      return await _userService.UpdateUser(userId, user);
    }

    /// <summary>
    /// Deletes the current user.
    /// </summary>
    /// <param name="userId">Of the user to delete</param>
    /// <returns>The result of the deletion</returns>
    [HttpDelete("{userId}")]
    [Authorize(Policy = "write")]
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