
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Models;

namespace Flooq.Identity.Service
{
  public class FlooqProfileService : IProfileService
  {
    // this method adds claims that should go into the token to context.IssuedClaims
    public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
      var requestedClaimTypes = context.RequestedClaimTypes;
      var user = context.Subject;

      context.IssuedClaims.AddRange(user.Claims);

      return Task.CompletedTask;
    }

    // this method allows to check if the user is still "enabled" per token request
    public virtual Task IsActiveAsync(IsActiveContext context)
    {
      context.IsActive = true;
      return Task.CompletedTask;
    }
  }
}