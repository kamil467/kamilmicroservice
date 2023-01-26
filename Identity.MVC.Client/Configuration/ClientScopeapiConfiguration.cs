using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.MVC.Client.Configuration
{
    public  static class ClientScopeapiConfiguration
    {

        /// <summary>
        /// Identity Resource for User Interaction Flow
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {

                new IdentityResources.OpenId(), // it will add subject it to token.
                new IdentityResources.Profile(), // user profile information
                new IdentityResource  // custom resource
                {
                    Name ="verification",
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Email,  // this will be included in the token 
                        JwtClaimTypes.EmailVerified  // this will be include in the token
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("catalog-api","Catalog API Services")

            };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //Client credentiaqls mode - Used for servie to service communication
                new Client
                {
                    ClientId ="postman",
                    ClientSecrets={ new Secret("postman-secret".Sha256()) }, // applying Sha512 Hash algorithmn
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalog-api" }
                }

            };
        }
    }
}
