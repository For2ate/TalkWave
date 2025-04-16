import { useState } from "react";
import styles from "./CreateChatForm.module.css";
import { StyledButton, StyledInput } from "Shared/Ui";
import { UserApiEndpoints } from "Shared/Api";
import { ChatApiEndpoints } from "Features/Chat/Api/ChatApiEndpoints";
import { useAppDispatch } from "Shared/Lib";
import { addChat } from "Features/Chat/Model/Slices/ChatSlice";
import { MessageApiEndpoints } from "Features/Chat/Api/MessageApiEndpoints";

export const CreateChatForm = (params: { closeModal: () => void }) => {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");

  const dispatch = useAppDispatch();

  const handleSubmit = async () => {
    if (!message || !email) return;

    const user = await UserApiEndpoints.getUserByEmail(email);
    debugger;

    if (user) {
      const request = {
        senderUserId: localStorage["userId"],
        recipientUserId: user.id,
        message: message,
      };

      const response = await ChatApiEndpoints.createPersonalChat(request);

      if (response && response.lastMessageId) {
        const lastMessage = await MessageApiEndpoints.getMessageById(
          response.lastMessageId
        );

        let chat = {
          id: response.id,
          name: response.name,
          isGroupChat: response.isGroupChat,
          createdAt: response.createdAt,
          createdBy: response.createdBy,
          role: response.role,
          lastMessage,
          chatMembers: response.chatMembers,
        };

        dispatch(addChat(chat));
      }
    }

    setEmail("");
    setMessage("");
    await params.closeModal();
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
          label="Sending message"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        ></StyledInput>
        <div>
          <StyledButton onClick={handleSubmit}>
            <div>Create Chat</div>
          </StyledButton>
        </div>
      </div>
    </div>
  );
};
