using AuthServer.Security;
using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServer.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("AuthServerAPI", "Auth Server API", new[]{ ExtendedClaimTypes.ProviderUserId, JwtClaimTypes.Email, JwtClaimTypes.Name })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "auth_server_api_swagger",
                    ClientName = "Swagger UI for Auth Server API",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    //过期时间24小时
                    AccessTokenLifetime = 60*60*24,
                    RedirectUris =
                    {
                        "https://localhost:44323/oauth2-redirect.html",     // Local Debug
                    },
                    AllowedScopes = { "AuthServerAPI"}
                }
            };
        }
    }
}
