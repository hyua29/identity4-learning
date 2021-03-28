namespace IdentityServer.Temp
{
    using System.Collections.Generic;
    using IdentityServer4.Models;
    using ResourceWebApp.Contracts;

    internal static class Resources
    { 
        /// <summary>
        /// This specify what clients can know about the users
        /// </summary>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"user", "admin"}
                }
            };
        }

        /// <summary>
        /// An API resource allows you to model access to an entire protected resource, an API, with individual permissions levels (scopes) that a client application can request access to.
        /// </summary>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = Constants.Name,
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string>
                        {"api1.admin.read", "api1.admin.write", "api1.user.read", "api1.user.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1.admin.read", "Read Access to API #1"),
                new ApiScope("api1.admin.write", "Write Access to API #1"),
                new ApiScope("api1.user.read", "Read Access to API #1"),
                new ApiScope("api1.user.write", "Write Access to API #1")
            };
        }
    }
}