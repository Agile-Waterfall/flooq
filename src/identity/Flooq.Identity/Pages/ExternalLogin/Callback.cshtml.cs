using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using IdentityModel;
using Flooq.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flooq.Identity.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Callback : PageModel
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly ILogger<Callback> _logger;
  private readonly IEventService _events;

  public Callback(
    UserManager<ApplicationUser> userManager,
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Callback> logger,
    TestUserStore users = null)
  {
    _userManager = userManager;
    _interaction = interaction;
    _logger = logger;
    _events = events;
  }

  public async Task<IActionResult> OnGet()
  {
    var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
    if (result?.Succeeded != true)
    {
      throw new Exception("External authentication error");
    }

    var externalUser = result.Principal;

    if (_logger.IsEnabled(LogLevel.Debug))
    {
      var externalClaims = externalUser.Claims.Select(c => $"{c.Type}: {c.Value}");
      _logger.LogDebug("External claims: {@claims}", externalClaims);
    }

    var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result);
    if (user == null)
    {
      user = await AutoProvisionUserAsync(provider, providerUserId, claims);
    }

    var additionalLocalClaims = new List<Claim>();
    var localSignInProps = new AuthenticationProperties();
    CaptureExternalLoginContext(result, additionalLocalClaims, localSignInProps);

    var isuser = new IdentityServerUser(user.Id)
    {
      DisplayName = user.UserName,
      IdentityProvider = provider,
      AdditionalClaims = additionalLocalClaims
    };

    await HttpContext.SignInAsync(isuser, localSignInProps);
    await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

    var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";
    var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
    await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id, user.UserName, true, context?.Client.ClientId));

    if (context != null)
    {
      if (context.IsNativeClient())
      {
        return this.LoadingPage(returnUrl);
      }
    }

    return Redirect(returnUrl);
  }

  private void CaptureExternalLoginContext(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
  {
    var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
    if (sid != null)
    {
      localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
    }

    var idToken = externalResult.Properties.GetTokenValue("id_token");
    if (idToken != null)
    {
      localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
    }
  }

  private async Task<ApplicationUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
  {
    var filtered = new List<Claim>();
    var name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
    if (name != null)
    {
      filtered.Add(new Claim(JwtClaimTypes.Name, name));
    }
    else
    {
      var first = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
      var last = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
      if (first != null && last != null)
      {
        filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
      }
      else if (first != null)
      {
        filtered.Add(new Claim(JwtClaimTypes.Name, first));
      }
      else if (last != null)
      {
        filtered.Add(new Claim(JwtClaimTypes.Name, last));
      }
    }

    var email = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
       claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
    if (email != null)
    {
      filtered.Add(new Claim(JwtClaimTypes.Email, email));
    }

    var user = new ApplicationUser
    {
      UserName = name ?? Guid.NewGuid().ToString(),
      Email = email
    };
    var identityResult = await _userManager.CreateAsync(user);
    if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

    if (filtered.Any())
    {
      identityResult = await _userManager.AddClaimsAsync(user, filtered);
      if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);
    }

    identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
    if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

    identityResult = await _userManager.AddToRoleAsync(user, "Free");
    if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

    return user;
  }

  private async Task<(ApplicationUser user, string provider, string providerUserId, IEnumerable<Claim> claims)> FindUserFromExternalProviderAsync(AuthenticateResult result)
  {
    var externalUser = result.Principal;
    var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                      externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                      throw new Exception("Unknown userid");

    var claims = externalUser.Claims.ToList();
    claims.Remove(userIdClaim);

    var provider = result.Properties.Items["scheme"];
    var providerUserId = userIdClaim.Value;

    var user = await _userManager.FindByLoginAsync(provider, providerUserId);

    return (user, provider, providerUserId, claims);
  }
}