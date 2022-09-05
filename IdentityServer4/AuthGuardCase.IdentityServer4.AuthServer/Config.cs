using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;

namespace AuthGuardCase.IdentityServer4.AuthServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("Employee_Service")
                {
                    Scopes={ "Employee_Service.Read","Employee_Service.Write" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("Employee_Service.Read"),
                new ApiScope("Employee_Service.Write"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="Client1",
                    ClientName="Client1 Web App",
                    ClientSecrets=new[]{ new Secret("Secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "Employee_Service.Read" }
                },

                new Client()
                {
                    ClientId="Client2",
                    ClientName="Client2 Web App",
                    ClientSecrets=new[]{ new Secret("Secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "Employee_Service.Read", "Employee_Service.Write" }
                },

                  new Client()
                {
                    ClientId="Client3",
                    ClientName="Client3 Web App",
                    ClientSecrets=new[]{ new Secret("Secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ null }
                },
            };
        }
    }
}