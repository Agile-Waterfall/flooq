
using Microsoft.AspNetCore.Identity;
using Flooq.Identity.Models;

namespace Flooq.Identity.Services;

/// <summary>
/// Responsible for handling changes to the user accounts
/// </summary>
public interface IUserService
{
  /// <summary>
  /// Get a user by Id
  /// </summary>
  /// <param name="userId">Of the user to return</param>
  /// <returns>User with the given Id</returns>
  Task<ApplicationUser> GetUsersById(string userId);

  /// <summary>
  /// Update a user
  /// </summary>
  /// <param name="userId">Of the user to update</param>
  /// <param name="newUser">Object with the new values</param>
  /// <returns>The result of the operation</returns>
  Task<IdentityResult> UpdateUser(string userId, ApplicationUser newUser);

  /// <summary>
  /// Deletes a user
  /// </summary>
  /// <param name="user">to delete</param>
  /// <returns>The result of the operation</returns>
  Task<IdentityResult> DeleteUser(ApplicationUser user);

  /// <summary>
  /// Gets the roles of a user.
  /// </summary>
  /// <param name="userId">Of the user to get the role of</param>
  /// <returns>Names of the roles of the user having the given Id</returns>
  Task<IList<string>> GetUserRoles(string userId);

  /// <summary>
  /// Adds a user to a role.
  /// </summary>
  /// <param name="userId">of the user to add to the role</param>
  /// <param name="roleName">of the role to add the user to</param>
  /// <returns>The result of the operation</returns>
  Task<IdentityResult> AddUserToRole(string userId, string roleName);

  /// <summary>
  /// Removes a user from a role.
  /// </summary>
  /// <param name="userId">of the user to remove from the role</param>
  /// <param name="roleName">of the role to remove the user from</param>
  /// <returns>The result of the operation</returns>
  Task<IdentityResult> RemoveUserFromRole(string userId, string roleName);
}