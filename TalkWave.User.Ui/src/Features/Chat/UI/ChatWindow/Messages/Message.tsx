import { MessageModel } from "Entities/Messages/MessageTypes";
import styles from "./Message.module.css";

interface Props {
  message: MessageModel;
  isCurrentUser: boolean;
}

export const Message = ({ message, isCurrentUser }: Props) => {
  const messageTime = new Date(message.sentAt).toLocaleTimeString([], {
    hour: "2-digit",
    minute: "2-digit",
  });

  const fullDate = new Date(message.sentAt).toLocaleString("ru-RU", {
    day: "numeric",
    month: "long",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    second: "2-digit",
  });

  return (
    <div
      className={`${styles.message} ${
        isCurrentUser ? styles.currentUser : styles.otherUser
      }`}
    >
      <div className={styles.content}>{message.content}</div>
      <div className={styles.time} title={fullDate}>
        {messageTime}
      </div>
    </div>
  );
};
