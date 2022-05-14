using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Flooq.Identity.Data;
using Flooq.Identity.Models;

namespace Flooq.Identity.Services;

public class UserService : IUserService
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly FlooqIdentityContext _context;

  public UserService(FlooqIdentityContext context, UserManager<ApplicationUser> userManager)
  {
    _context = context;
    _userManager = userManager;
  }

  public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<ApplicationUser> GetUsersById(string userId)
  {
    return await _context.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
  }

  public async Task<IdentityResult> SetUserName(ApplicationUser user, string userName)
  {
    return await _userManager.SetUserNameAsync(user, userName);
  }

  public async Task<IdentityResult> DeleteUser(ApplicationUser user)
  {
    return await _userManager.DeleteAsync(user);
  }
}