using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;


namespace Chat.Identity;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("Chat.Friends", "Friends API"),
            new("Chat.Server", "Server API")
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
            new("Chat.Friends", "Friends API", new []
            {
                JwtClaimTypes.Name
            })
            {
                Scopes = {"Chat.Friends"}
            },
            new("Chat.Server", "Server API", new []
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
            new()
            {
                ClientId = "chat-client",
                ClientName = "Chat Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://client/signin-callback"
                },
                AllowedCorsOrigins =
                {
                    ""
                },
                PostLogoutRedirectUris =
                {
                    "http://client/signout-callback"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Chat.Friends",
                    "Chat.Server"
                },
                AllowAccessTokensViaBrowser = true
            },
            new()
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
            new()
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