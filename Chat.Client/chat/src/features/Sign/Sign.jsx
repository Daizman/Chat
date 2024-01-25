import React, {useState, useRef} from "react";
import {Button} from "react-bootstrap";
// import SignModal from "./SignModal/SignModal";


const Sign = () => {
    const 
        [showModal, setShowModal] = useState(false),
        signtype = useRef("login"),
        apply = () => {
            setShowModal(false);
        },
        onSignInClick = () => {
            signtype.current = "login";
            setShowModal(true);
        },
        onSignUpClick = () => {
            signtype.current = "register";
            setShowModal(true);
        };
    
    return (
         <div>
            <Button onClick={onSignInClick} variant="primary">Войти</Button>
            <Button onClick={onSignUpClick} variant="secondary">Зарегистрироваться</Button>
            {/*<SingModal signtype={signtype.current} apply={apply} show={showModal}/>*/}
         </div>
    );   
};

export default Sign;
