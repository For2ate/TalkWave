import { Link } from "react-router-dom";
import styles from "./ChatListItem.module.css";
import { Chat } from "Entities/Chats/ChatTypes";
import { useAppDispatch } from "Shared/Lib";
import { setSelectedChat } from "Features/Chat/Model";

interface Props {
  chat: Chat;
}

export const ChatListItem = ({ chat }: Props) => {
  const dispatch = useAppDispatch();

  const handleClick = async () => {
    await dispatch(setSelectedChat(chat.id));
  };

  return (
    <div className={styles.layout}>
      <Link
        to={`/chats/${chat.id}`}
        className={styles.chatButton}
        onClick={handleClick}
      >
        <h2 className={styles.chatName}>{chat.name}</h2>
        {chat.lastMessage && (
          <div className={styles.messageContainer}>
            <p className={styles.message}>
              {chat.lastMessage.content.length > 50
                ? `${chat.lastMessage.content.substring(0, 20)}...`
                : chat.lastMessage.content}
            </p>
            <p className={styles.data}>
              {new Date(chat.lastMessage.sentAt).toLocaleTimeString([], {
                hour: "2-digit",
                minute: "2-digit",
              })}
            </p>
          </div>
        )}
      </Link>
    </div>
  );
};
