using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Flooq.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flooq.Identity.Pages.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IClientStore _clientStore;
  private readonly IEventService _events;
  private readonly IAuthenticationSchemeProvider _schemeProvider;
  private readonly IIdentityProviderStore _identityProviderStore;

  public ViewModel View { get; set; }

  [BindProperty]
  public InputModel Input { get; set; }

  public Index(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IIdentityServerInteractionService interaction,
    IClientStore clientStore,
    IAuthenticationSchemeProvider schemeProvider,
    IIdentityProviderStore identityProviderStore,
    IEventService events)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _interaction = interaction;
    _clientStore = clientStore;
    _schemeProvider = schemeProvider;
    _identityProviderStore = identityProviderStore;
    _events = events;
  }

  public async Task<IActionResult> OnGet(string returnUrl)
  {
    await BuildModelAsync(returnUrl);

    if (View.IsExternalLoginOnly)
    {
      return RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl });
    }

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
    if (Input.Button != "login")
    {
      if (context != null)
      {
        await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);
        if (context.IsNativeClient())
        {
          return this.LoadingPage(Input.ReturnUrl);
        }

        return Redirect(Input.ReturnUrl);
      }
      else
      {
        return Redirect("~/");
      }
    }

    if (ModelState.IsValid)
    {
      var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, lockoutOnFailure: true);
      if (result.Succeeded)
      {
        var user = await _userManager.FindByNameAsync(Input.Username);
        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

        if (context != null)
        {
          if (context.IsNativeClient())
          {
            return this.LoadingPage(Input.ReturnUrl);
          }
          return Redirect(Input.ReturnUrl);
        }

        if (Url.IsLocalUrl(Input.ReturnUrl))
        {
          return Redirect(Input.ReturnUrl);
        }
        else if (string.IsNullOrEmpty(Input.ReturnUrl))
        {
          return Redirect("~/");
        }
        else
        {
          throw new Exception("invalid return URL");
        }
      }

      await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials", clientId: context?.Client.ClientId));
      ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
    }

    await BuildModelAsync(Input.ReturnUrl);
    return Page();
  }

  private async Task BuildModelAsync(string returnUrl)
  {
    Input = new InputModel
    {
      ReturnUrl = returnUrl
    };

    var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
    if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
    {
      var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;

      View = new ViewModel
      {
        EnableLocalLogin = local,
      };

      Input.Username = context?.LoginHint;

      if (!local)
      {
        View.ExternalProviders = new[] { new ViewModel.ExternalProvider { AuthenticationScheme = context.IdP } };
      }

      return;
    }

    var schemes = await _schemeProvider.GetAllSchemesAsync();

    var providers = schemes
      .Where(x => x.DisplayName != null)
      .Select(x => new ViewModel.ExternalProvider
      {
        DisplayName = x.DisplayName ?? x.Name,
        AuthenticationScheme = x.Name
      }).ToList();

    var dyanmicSchemes = (await _identityProviderStore.GetAllSchemeNamesAsync())
      .Where(x => x.Enabled)
      .Select(x => new ViewModel.ExternalProvider
      {
        AuthenticationScheme = x.Scheme,
        DisplayName = x.DisplayName
      });
    providers.AddRange(dyanmicSchemes);


    var allowLocal = true;
    if (context?.Client.ClientId != null)
    {
      var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
      if (client != null)
      {
        allowLocal = client.EnableLocalLogin;

        if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
        {
          providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
        }
      }
    }

    View = new ViewModel
    {
      AllowRememberLogin = LoginOptions.AllowRememberLogin,
      EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin,
      ExternalProviders = providers.ToArray()
    };
  }
}