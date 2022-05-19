
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
}