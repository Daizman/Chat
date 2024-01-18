using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;


namespace Chat.Identity;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Chat.Friends", "Friends API"),
            new ApiScope("Chat.Server", "Server API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("Chat.Friends", "Friends API", new []
            {
                JwtClaimTypes.Name
            })
            {
                Scopes = {"Chat.Friends"}
            },
            new ApiResource("Chat.Server", "Server API", new []
            {
                JwtClaimTypes.Name
            })
            {
                Scopes = {"Chat.Server"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "chat-friends",
                ClientName = "Chat Friends",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://friends/singin-oidc"
                },
                AllowedCorsOrigins =
                {
                    ""
                },
                PostLogoutRedirectUris =
                {
                    "http://friends/signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Chat.Friends"
                },
                AllowAccessTokensViaBrowser = true
            },
            new Client
            {
                ClientId = "chat-server",
                ClientName = "Chat Server",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://server/singin-oidc"
                },
                AllowedCorsOrigins =
                {
                    ""
                },
                PostLogoutRedirectUris =
                {
                    "http://server/signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Chat.Server"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}