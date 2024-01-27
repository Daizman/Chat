import React, {useState, useRef} from "react";
import { Col, Container, Row } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import SignModal from "./SignModal/SignModal";


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
        <Container className="m-auto w-100" fluid>
            <Row>
                <Col className="text-center">
                    <Button onClick={onSignInClick} variant="primary">Войти</Button>
                </Col>
            </Row>
            <Row className="mt-2">
                <Col className="text-center">
                    <Button onClick={onSignUpClick} variant="secondary">Зарегистрироваться</Button>
                </Col>
            </Row>
            <SignModal signtype={signtype.current} apply={apply} show={showModal}/>
        </Container>
    );   
};

export default Sign;
