import { ChatSidebar } from "./ChatSidebar";

import styles from "./Chats.module.css";

export const Chats = () => {
  const chats = ["Alice", "Bob", "Charlie", "David"];

  return (
    <div className={styles.layout}>
      <main>
        {chats.map((chat, index) => (
          <ChatSidebar key={index} chat={chat} />
        ))}
      </main>
    </div>
  );
};
