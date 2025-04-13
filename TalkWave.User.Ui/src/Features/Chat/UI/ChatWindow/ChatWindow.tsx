import { useParams } from "react-router-dom";
import styles from "./ChatWindow.module.css";
import { InputBox } from "./InputBox";
import { useAppDispatch, useChatHub } from "Shared/Lib/Hooks";
import { Messages } from "./Messages/Messages";

export const ChatWindow = () => {
  const { chatId } = useParams<{ chatId: string }>();
  const dispatch = useAppDispatch();

  const hub = useChatHub();

  const handleSendMessage = async (message: string) => {
    if (!hub) return;

    const request = {
      senderId: localStorage["userId"],
      chatId: chatId,
      content: message,
    };

    await hub.invoke("SendMessage", request);
  };

  return (
    <div className={styles.layout}>
      <header className={styles.header}>
        <h1>Чат {chatId}</h1>
      </header>
      <main className={styles.main}>
        {chatId && <Messages chatId={chatId} />}
      </main>
      <footer className={styles.footer}>
        <InputBox onSend={handleSendMessage} />
      </footer>
    </div>
  );
};
