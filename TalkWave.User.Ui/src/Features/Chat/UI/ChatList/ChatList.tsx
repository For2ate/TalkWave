import { ChatListItem } from "./ChatListItem";
import styles from "./ChatsList.module.css";

interface Chat {
  id: string;
  name: string;
  lastMessage?: string;
}

export const ChatList = () => {
  const chats: Chat[] = [
    { id: "1", name: "Alice", lastMessage: "Привет!" },
    { id: "2", name: "Bob", lastMessage: "Как дела?" },
    { id: "3", name: "Charlie", lastMessage: "До встречи" },
    { id: "4", name: "David", lastMessage: "Отправляю файл" },
  ];

  return (
    <div className={styles.layout}>
      <main>
        {chats.map((chat) => (
          <ChatListItem key={chat.id} chat={chat} />
        ))}
      </main>
    </div>
  );
};
