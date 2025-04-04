import { Link } from "react-router-dom";
import styles from "./ChatListItem.module.css";
import { Chat } from "Features/Chat/Model/Types/Chat";

interface Props {
  chat: Chat;
}

export const ChatListItem = ({ chat }: Props) => {
  return (
    <div className={styles.layout}>
      <Link to={`/chats/${chat.id}`} className={styles.chatButton}>
        <h2>{chat.name}</h2>
        {chat.lastMessage && (
          <p className={styles.lastMessage}>{chat.lastMessage.content}</p>
        )}
      </Link>
    </div>
  );
};
