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

  /// <summary>
  /// Gets the role of a user.
  /// </summary>
  /// <param name="userId">Of the user to get the role of</param>
  /// <returns>Names of the roles of the user having the given Id</returns>
  public async Task<IList<string>> GetUserRoles(string userId)
  {
    var user = await GetUsersById(userId);
    return await _userManager.GetRolesAsync(user);
  }

  /// <summary>
  /// Adds a user to a role.
  /// </summary>
  /// <param name="userId">of the user to add to the role</param>
  /// <param name="roleName">of the role to add the user to</param>
  /// <returns>The result of the operation</returns>
  public async Task<IdentityResult> AddUserToRole(string userId, string roleName)
  {
    var user = await GetUsersById(userId);
    return await _userManager.AddToRoleAsync(user, roleName);
  }

  /// <summary>
  /// Removes a user from a role.
  /// </summary>
  /// <param name="userId">of the user to remove from the role</param>
  /// <param name="roleName">of the role to remove the user from</param>
  /// <returns>The result of the operation</returns>
  public async Task<IdentityResult> RemoveUserFromRole(string userId, string roleName)
  {
    var user = await GetUsersById(userId);
    return await _userManager.RemoveFromRoleAsync(user, roleName);
  }
}