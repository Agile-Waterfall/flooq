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
              ClientId = "editor",
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              ClientSecrets =
              {
                  // TODO: Use generated secred here
                  new Secret("secret".Sha256())
              },
              AllowedScopes = { "flooqapi" },
              AllowedCorsOrigins = { "http://localhost:8080"}
            },
            new Client
            {
                ClientId = "web",
                // TODO: Use generated secred here
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // TODO: Use application urls
                RedirectUris =
                {
                  "http://localhost:8080/swagger/oauth2-redirect.html",
                  "http://localhost:3000/api/auth/callback/flooq"
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