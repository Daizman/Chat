import { UserManager } from "oidc-client";

const config = {
    authority: process.env.REACT_APP_IDENTITY_URL,
    client_id: "chat-client",
    redirect_uri: "http://client/signin-callback",
    response_type: "code",
    scope: "openid profile openid profile Chat.Friend Chat.Server",
    post_logout_redirect_uri: "https://client/signout-callback"
};

const userManager = new UserManager(config);

function getUser() {
    return userManager.getUser();
}

export {
    getUser,
};
