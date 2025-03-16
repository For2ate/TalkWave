import { useState } from "react";
import styles from "./Login.module.css";
import { StyledInput } from "Shared/Ui";

export const LoginPage = () => {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");

  const handleLoginChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLogin(event.target.value); // update login state
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value); // update password state
  };

  return (
    <>
      <div className={styles.layout}>
        <main className={styles.container}>
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
          </div>
        </main>
      </div>
    </>
  );
};
