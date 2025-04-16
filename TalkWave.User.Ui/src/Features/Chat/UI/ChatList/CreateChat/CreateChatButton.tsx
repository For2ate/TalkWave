import { useState } from "react";
import { Modal, StyledButton } from "Shared/Ui";
import styles from "./CreateChat.module.css";
import { CreateChatForm } from "./CreateChatForm";

export const CreateChatButton = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const closeModal = async () => {
    setIsModalOpen(false);
  };

  return (
    <div className={styles.layout}>
      <StyledButton onClick={() => setIsModalOpen(true)}>
        <div>Create chat</div>
      </StyledButton>

      <Modal isOpen={isModalOpen} onClose={closeModal} title="Create New Chat">
        <CreateChatForm closeModal={closeModal} />
      </Modal>
    </div>
  );
};
