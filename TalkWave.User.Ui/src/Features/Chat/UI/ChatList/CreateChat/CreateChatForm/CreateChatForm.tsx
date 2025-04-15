import { useState } from "react";
import styles from "./CreateChatForm.module.css";
import { StyledButton, StyledInput } from "Shared/Ui";

export const CreateChatForm = () => {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
  };

  return (
    <div className={styles.layout}>
      <div className={styles.container}>
        <StyledInput
          label="User email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        ></StyledInput>
        <StyledInput
          label="Send message"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        ></StyledInput>
        <div>
          <StyledButton
            onClick={() => {
              handleSubmit;
            }}
          >
            <div>Create Chat</div>
          </StyledButton>
        </div>
      </div>
    </div>
  );
};
