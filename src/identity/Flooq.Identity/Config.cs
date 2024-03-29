﻿using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Flooq.Identity;

/// <summary>
/// Configures the Authentication Clients as well as the available Scopes and Resources
/// </summary>
public static class Config
{
  /// <summary>
  /// Resources that can be accessed by an authenticated user.
  /// </summary>
  /// <value>A list of available resources</value>
  public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

  /// <summary>
  /// Scopes that a user can have. From these scopes the access policies will be derived
  /// </summary>
  /// <value>A list of available scopes</value>
  public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
          {
              new ApiScope(name: "write", displayName: "Write Access"),
              new ApiScope(name: "read", displayName: "Read Access"),
              new ApiScope(name: "read_all", displayName: "Read All Access")
           };

  /// <summary>
  /// Configuration of the clients that can be authenticated with the identity server.
  /// </summary>
  /// <value>A list of available clients</value>
  public static IEnumerable<Client> Clients =>
      new Client[]
          {
            new Client {
              ClientId = "executor",
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              ClientSecrets =
              {
                  new Secret(Environment.GetEnvironmentVariable("IDENTITY_SERVER_CLIENT_SECRET").Sha256())
              },
              AllowedScopes = { "read", "read_all" },
              AllowedCorsOrigins = { "http://localhost:8080", "http://localhost:3500",  "https://localhost:5001", "https://api-staging.flooq.io", "https://executor-staging.flooq.io", "https://executor.flooq.io"  }
            },
            new Client
            {
              ClientId = "web",
              ClientSecrets = { new Secret(Environment.GetEnvironmentVariable("IDENTITY_SERVER_CLIENT_SECRET").Sha256()) },

              AllowedGrantTypes = GrantTypes.Code,

              RedirectUris =
              {
                "https://localhost:5001/swagger/oauth2-redirect.html",
                "http://localhost:8080/swagger/oauth2-redirect.html",
                "http://localhost:3000/api/auth/callback/flooq",

                "https://api-staging.flooq.io/swagger/oauth2-redirect.html",
                "https://identity-staging.flooq.io/swagger/oauth2-redirect.html",
                "https://editor-staging.flooq.io/api/auth/callback/flooq",
                "https://identity-staging.flooq.io/signin-github",

                "https://editor.flooq.io/api/auth/callback/flooq",
                "https://identity.flooq.io/signin-github",
              },
              RequirePkce = false,
              AllowOfflineAccess = true,
              AllowedScopes = new List<string>
              {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "read",
                  "write",
                  "offline_access"
              },
              AlwaysIncludeUserClaimsInIdToken = true,
              PostLogoutRedirectUris = { "http://localhost:3000/logout", "https://editor-staging.flooq.io/logout", "https://editor.flooq.io/logout" },
              AllowedCorsOrigins = { "http://localhost:3000", "http://localhost:8080", "https://localhost:5001", "https://identity-staging.flooq.io", "https://api-staging.flooq.io", "https://executor-staging.flooq.io", "https://executor.flooq.io", "https://identity.flooq.io"  },
              AccessTokenLifetime = 60 * 60 * 4,
              AuthorizationCodeLifetime = 60 * 60
            }
          };
}