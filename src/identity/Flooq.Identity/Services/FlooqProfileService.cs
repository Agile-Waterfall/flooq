using System.Security.Claims;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Flooq.Identity.Models;

namespace Flooq.Identity.Services
{
  public class FlooqProfileService : IProfileService
  {
    protected UserManager<ApplicationUser> _userManager;

    public FlooqProfileService(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
      var user = await _userManager.GetUserAsync(context.Subject);
      var claims = new List<Claim>();

      if (user?.Email != null)
      {
        claims.Add(new Claim("email", user.Email));
      }
      if (user?.UserName != null)
      {
        claims.Add(new Claim("username", user.UserName));
      }

      context.IssuedClaims.AddRange(claims);
    }

    public virtual Task IsActiveAsync(IsActiveContext context)
    {
      context.IsActive = true;
      return Task.CompletedTask;
    }
  }
}