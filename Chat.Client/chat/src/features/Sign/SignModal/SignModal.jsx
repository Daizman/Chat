import React from "react";
import {Button, Modal, Form} from "react-bootstrap";
import { login } from "../../../app/Identity/authService";
import { postRegister } from "../../../api/Identity/webapi";

const SignModal = (props) => {
    const 
        headers = {
            "login": "Вход",
            "register": "Регистрация"
        },
        apply = (event) => {
            const
                formData = new FormData(event.target),
                name = formData.get("name"),
                password = formData.get("password"),
                confirmPassword = formData.get("confirmPassword");

            if (props.signtype === "login")
                login();
            else
                postRegister(name, password, confirmPassword);
        };
    
    return (
        <Modal
            show={props.show}
            onHide={props.closeHandle}
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title>{headers[props.signtype]}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form method="POST" onSubmit={apply}>
                    <Form.Control 
                        className="mb-1" 
                        type="text" 
                        placeholder="Введите логин" 
                        name="name"
                        controlId="name"
                    />
                    <Form.Control 
                        className="mb-1" 
                        type="password" 
                        placeholder="Введите пароль" 
                        name="password"
                        controlId="password"
                    />
                    {
                        props.signtype === "register" 
                        && <Form.Control 
                            className="mb-1" 
                            type="password" 
                            placeholder="Повторите пароль" 
                            name="confirmPassword"
                            controlId="confirmPassword"
                        />
                    }
                    <Button variant="primary" type="submit">Подтвердить</Button>
                </Form>
            </Modal.Body>
        </Modal>
    );
};

export default SignModal;
