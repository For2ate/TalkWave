import { useState } from "react";
import { Modal, StyledButton } from "Shared/Ui";
import styles from "./CreateChat.module.css";
import { CreateChatForm } from "./CreateChatForm";

export const CreateChatButton = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <div className={styles.layout}>
      <StyledButton onClick={() => setIsModalOpen(true)}>
        <div>Create chat</div>
      </StyledButton>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title="Create New Chat"
      >
        <CreateChatForm />
      </Modal>
    </div>
  );
};
