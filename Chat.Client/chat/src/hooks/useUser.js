import { useState, useEffect } from 'react';
import authService from "../app/Identity/authService";

export default function useUser() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        authService.getUser().then(user => {
            if (user && !user.expired) {
                setUser(user);
            }
        });
    }, []);

    return user;
}
