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
        <h2>{chat.name}</h2>
        {chat.lastMessage && (
          <p className={styles.lastMessage}>{chat.lastMessage.content}</p>
        )}
      </Link>
    </div>
  );
};
