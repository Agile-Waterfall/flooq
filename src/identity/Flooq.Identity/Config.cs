using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using System;

namespace IdentityServer;

public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

  public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
          {
              new ApiScope(name: "flooqapi", displayName: "FlooqApi")
           };

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
              AllowedScopes = { "flooqapi" },
              AllowedCorsOrigins = { "http://localhost:8080", "https://api-staging.flooq.io", "https://executor-staging.flooq.io", "https://executor.flooq.io"  }
            },
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret(Environment.GetEnvironmentVariable("IDENTITY_SERVER_CLIENT_SECRET").Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris =
                {
                  "http://localhost:8080/swagger/oauth2-redirect.html",
                  "http://localhost:3000/api/auth/callback/flooq",

                  "https://api-staging.flooq.io/swagger/oauth2-redirect.html",
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
                    "flooqapi"
                }
            }
          };
}