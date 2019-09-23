using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ClashRoyaleApi.Sts.Helpers
{
    public class StsConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "clashroyale-api",
                    DisplayName = "Clash Royale API",
                    Description = "Clash Royale API Resource",
                    Scopes =
                    {
                        new Scope("cr_api.read_only", "Read only API scope")
                        {
                            Description = "Can only retrieve data from API resource."
                        },
                        new Scope("cr_api.read_write", "Read and write API scope")
                        {
                            Description = "Can add, update and retrieve data from API resouce."
                        },
                        new Scope("cr_api.admin", "Admin API scope")
                        {
                            Description = "Can do all stuff."
                        }
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "clashroyale-client",
                    ClientName = "Clash Royale SPA & PWA",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:4200/assets/oidc-login-redirect.html", "http://localhost:4200/assets/silent-redirect.html", "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:4200/?postLogout=true" },
                    //AllowedCorsOrigins =     { "http://localhost:4200/" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "cr_api.read_only",
                        "cr_api.read_write",
                        "cr_api.admin"
                    },
                    IdentityTokenLifetime = 120,
                    AccessTokenLifetime = 120
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "https://localhost:4201/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:4201/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "custom.profile",
                    DisplayName = "Custom Profile",
                    Description = "Other profile information not in the default profile",
                    UserClaims = new[] { "name", "email", "status" }
                }
            };
        }
    }
}