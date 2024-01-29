import axios from "axios";
import { getIdentityUrl } from "./config";

const
    postRegister = (name, password, confirmPassword) => {
        axios
            .post(`${getIdentityUrl()}/Register`, {
                name: name,
                password: password,
                confirmPassword: confirmPassword
            }).then(res => {
                console.log(res);
            })
            .catch(res => {
                console.error(res);
            });
    };


export {
    postRegister
};
