﻿import React from "react";
import {Button, Modal, Form} from "react-bootstrap";

const SignModal = (props) => {
    const 
        signtype = props.signtype,
        headers = {
            "login": "Вход",
            "register": "Регистрация"
        };
    
    return (
        <Modal
            {...props}
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                {headers[signtype]}
            </Modal.Header>
            <Modal.Body>
                <Form.Control className="mb-1" type="text" placeholder="Введите логин" />
                <Form.Control className="mb-1" type="password" placeholder="Введите пароль" />
                {signtype === "register" && <Form.Control type="password" placeholder="Повторите пароль" />}
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={props.apply}>Подтвердить</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default SignModal;
