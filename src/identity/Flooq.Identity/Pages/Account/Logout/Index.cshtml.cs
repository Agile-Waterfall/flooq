using System.Threading.Tasks;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using IdentityModel;
using Flooq.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flooq.Identity.Pages.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IEventService _events;

  [BindProperty]
  public string LogoutId { get; set; }

  public Index(SignInManager<ApplicationUser> signInManager, IIdentityServerInteractionService interaction, IEventService events)
  {
    _signInManager = signInManager;
    _interaction = interaction;
    _events = events;
  }

  public async Task<IActionResult> OnGet(string logoutId)
  {
    LogoutId = logoutId;

    var showLogoutPrompt = LogoutOptions.ShowLogoutPrompt;

    if (User?.Identity.IsAuthenticated != true)
    {
      showLogoutPrompt = false;
    }
    else
    {
      var context = await _interaction.GetLogoutContextAsync(LogoutId);
      if (context?.ShowSignoutPrompt == false)
      {
        showLogoutPrompt = false;
      }
    }

    if (showLogoutPrompt == false)
    {
      return await OnPost();
    }

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    if (User?.Identity.IsAuthenticated == true)
    {
      LogoutId ??= await _interaction.CreateLogoutContextAsync();

      await _signInManager.SignOutAsync();
      await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

      var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
      if (idp != null && idp != Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider)
      {
        if (await HttpContext.GetSchemeSupportsSignOutAsync(idp))
        {
          string url = Url.Page("/Account/Logout/Loggedout", new { logoutId = LogoutId });
          return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
        }
      }
    }

    return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });
  }
}