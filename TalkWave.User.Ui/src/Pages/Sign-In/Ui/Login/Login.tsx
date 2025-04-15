import { useState } from "react";
import styles from "./Login.module.css";
import { StyledButton, StyledInput } from "Shared/Ui";
import { LoginData } from "Entities/User/SignInModels";
import { Login } from "Pages/Sign-In/Api/SignIn";
import { useNavigate } from "react-router-dom";

export const LoginPage = () => {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLoginChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLogin(event.target.value); // update login state
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value); // update password state
  };
  const handleLogin = async () => {
    console.log(`login: ${login} password: ${password}`);
    const data: LoginData = { login, password };
    const result = await Login(data);
    if (result) {
      localStorage[`userId`] = result.id;
      localStorage[`userFirstName`] = result.firstName;
      localStorage[`userLastName`] = result.lastName;
      localStorage[`userEmail`] = result.email;
      navigate("/");
    } else {
      // login error
      console.error("Login failed:", result);
    }
  };

  return (
    <>
      <div className={styles.layout}>
        <main className={styles.container}>
          <h1>Sign In</h1>
          <div className={styles.box}>
            <StyledInput
              label="Login"
              type="input"
              onChange={handleLoginChange}
              value={login}
            />
            <StyledInput
              label="Password"
              type="password"
              onChange={handlePasswordChange}
              value={password}
            />
            <StyledButton onClick={handleLogin}>
              <p>Login</p>
            </StyledButton>
          </div>
          <p>
            <a href="/register"> Register </a>
          </p>
        </main>
      </div>
    </>
  );
};
