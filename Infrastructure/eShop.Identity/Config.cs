using Duende.IdentityServer.Models;

namespace eShop.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalogapi"),
            new ApiScope("basketapi"),
            new ApiScope("catalogapi.read"),
            new ApiScope("catalogapi.write"),
            new ApiScope("eshoppinggateway")
            

        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("Catalog","Catalog.Api")
            {
                Scopes = { "catalogapi.read", "catalogapi.write" }
            },
            new ApiResource("Basket", "Basket.Api")
            {
                Scopes = { "basketapi" }
            },
            new ApiResource ("EShoppingGateway","EShopping Gatwway")
            {
                Scopes = { "eshoppinggateway" , "basketapi" }
            }

        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "CatalogApiClient",
                ClientName = "Catalog Api Client",
                ClientSecrets = {new Secret ("134536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes ={ "catalogapi.write" , "catalogapi.read" }
            },
            new Client
            {
                ClientId = "BasketApiClient",
                ClientName = "Basket Api Client",
                ClientSecrets = {new Secret ("134536AA-A270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes ={ "basketapi" }
            },
            new Client
            {
                ClientId = "EShoppingGatewayClient",
                ClientName = "EShopping Gateway Client",
                ClientSecrets = {new Secret ("134536AA-A270-4058-80CA-8B99C992F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "basketapi", "eshoppinggateway" }
            }
            
        };
}
