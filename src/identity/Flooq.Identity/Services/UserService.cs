using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Flooq.Identity.Data;
using Flooq.Identity.Models;

namespace Flooq.Identity.Services;

/// <summary>
/// Responsible for handling changes to the user accounts
/// </summary>
public class UserService : IUserService
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly FlooqIdentityContext _context;

/// <summary>
/// Responsible for handling changes to the user accounts
/// </summary>
  public UserService(FlooqIdentityContext context, UserManager<ApplicationUser> userManager)
  {
    _context = context;
    _userManager = userManager;
  }
  /// <summary>
  /// Get a user by Id
  /// </summary>
  /// <param name="userId">Of the user to return</param>
  /// <returns>User with the given Id</returns>

  public async Task<ApplicationUser> GetUsersById(string userId)
  {
    return await _context.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
  }

  /// <summary>
  /// Update a user
  /// </summary>
  /// <param name="userId">Of the user to update</param>
  /// <param name="newUser">Object with the new values</param>
  /// <returns>The result of the operation</returns>
  public async Task<IdentityResult> UpdateUser(string userId, ApplicationUser newUser)
  {
      var userToChange = await GetUsersById(userId);
      userToChange.UserName = newUser.UserName;
      userToChange.Email = newUser.Email;

    return await _userManager.UpdateAsync(userToChange);
  }

  /// <summary>
  /// Deletes a user
  /// </summary>
  /// <param name="user">to delete</param>
  /// <returns>The result of the operation</returns>
  public async Task<IdentityResult> DeleteUser(ApplicationUser user)
  {
    return await _userManager.DeleteAsync(user);
  }
}