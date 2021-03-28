namespace IdentityServer.Temp
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    internal static class ClientStore
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                    AllowedScopes = new List<string> {"api1.admin.read"}
                }
                // ,
                // new Client
                // {
                //     ClientId = "oidcClient",
                //     ClientName = "Example Client Application",
                //     ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                //
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RedirectUris = new List<string> {"https://localhost:5002/signin-oidc"},
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.Email,
                //         "role",
                //         "api1.admin.read",
                //         "api1.user.read"
                //     },
                //
                //     RequirePkce = true,
                //     AllowPlainTextPkce = false
                // }
            };
        }
    }
}