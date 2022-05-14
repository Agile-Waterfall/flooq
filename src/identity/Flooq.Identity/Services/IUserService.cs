
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Flooq.Identity.Models;

namespace Flooq.Identity.Services;

public interface IUserService
{
  Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers();

  Task<ApplicationUser> GetUsersById(string userId);

  Task<IdentityResult> SetUserName(ApplicationUser user, string userName);

  Task<IdentityResult> DeleteUser(ApplicationUser user);
}