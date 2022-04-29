using Duende.IdentityServer.Models;
using Duende.IdentityServer;

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
                  // TODO: Use generated secred here
                  new Secret("secret".Sha256())
              },
              AllowedScopes = { "flooqapi" },
              AllowedCorsOrigins = { "https://api-staging.flooq.io", "https://executor-staging.flooq.io", "https://executor.flooq.io"  }
            },
            new Client
            {
                ClientId = "web",
                // TODO: Use generated secred here
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris =
                {
                  "https://api-staging/swagger/oauth2-redirect.html",
                  "https://localhost:3000/api/auth/callback/flooq",
                  "https://editor-staging.flooq.io/api/auth/callback/flooq",
                  "https://editor.flooq.io/api/auth/callback/flooq"
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