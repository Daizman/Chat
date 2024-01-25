import React from "react";
import useUser from "../hooks/useUser";
import Sign from "./Sign/Sign";

const Main = () => {
    const isUserLoggedIn = useUser() != null;
    
    return (
        <main>
            {
                isUserLoggedIn
                    ? "You are logged in."
                    : <Sign />
            }
        </main>
    )
};

export default Main;
