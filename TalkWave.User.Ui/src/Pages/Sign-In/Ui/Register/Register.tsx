import React from "react";

import { useState } from "react";
import styles from "./Register.module.css";
import { StyledButton, StyledInput } from "Shared/Ui";
import { RegisterData } from "Entities/User/SignInModels";
import { Register } from "Pages/Sign-In/Api/SignIn";
import { useNavigate } from "react-router-dom";

export const RegisterPage = () => {
  const [firstname, setFirstName] = useState("");
  const lastname = "";
  const [login, setLogin] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confpassword, setConfPassword] = useState("");

  const navigate = useNavigate();

  const handleFirstNameChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFirstName(event.target.value); // update first name state
  };

  const handleLoginChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLogin(event.target.value); // update login state
  };

  const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value); // update email state
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value); // update password state
  };

  const handleConfPasswordChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setConfPassword(event.target.value); // update confirm password state
  };

  const handleRegister = async () => {
    const data: RegisterData = { login, email, password, firstname, lastname };
    const result = await Register(data);
    if (result.success) {
      localStorage[`userId`] = result.data.id;
      localStorage[`userFirstName`] = result.data.firstname;
      localStorage[`userLastName`] = result.data.lastname;
      localStorage[`userEmail`] = result.data.email;
      navigate("/");
    } else {
      // Register error
      console.error("Register failed:", result.errors);
    }
  };

  return (
    <>
      <div className={styles.layout}>
        <main className={styles.container}>
          <h1>Register</h1>
          <div className={styles.box}>
            <StyledInput
              label="First Name"
              type="input"
              onChange={handleFirstNameChange}
              value={firstname}
            />
            <StyledInput
              label="Login"
              type="input"
              onChange={handleLoginChange}
              value={login}
            />
            <StyledInput
              label="Email"
              type="Email"
              onChange={handleEmailChange}
              value={email}
            />
            <StyledInput
              label="Password"
              type="password"
              onChange={handlePasswordChange}
              value={password}
            />
            <StyledInput
              label="Confirm password"
              type="password"
              onChange={handleConfPasswordChange}
              value={confpassword}
            />

            <StyledButton onClick={handleRegister}>
              <p>Register</p>
            </StyledButton>
          </div>
          <p>
            <a href="/login"> Login </a>
          </p>
        </main>
      </div>
    </>
  );
};
