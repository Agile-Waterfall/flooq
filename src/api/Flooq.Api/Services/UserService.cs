using Flooq.Api.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class UserService : IUserService
{
  private FlooqContext _context;

  public UserService(FlooqContext context)
  {
    _context = context;
  }

  public async Task<ActionResult<IdentityUser>> GetUserById(string? id)
  {
    var users = _context.Users;
    var actionResult = await users.FindAsync(id);
    return actionResult;
  }

  public async Task<ActionResult<IdentityUser>> GetUserByEmail(string? email)
  {
    var users = _context.Users;
    var actionResult = await users.Where(user => user.Email == email).FirstAsync();
    return actionResult;
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public EntityEntry<IdentityUser> AddUser(IdentityUser user)
  {
    return _context.Users.Add(user);
  }

  public EntityEntry<IdentityUser> RemoveUser(IdentityUser user)
  {
    return _context.Users.Remove(user);
  }

  public bool UserExists(string? id)
  {
    return _context.Users.Any(e => e.Id == id);
  }
}