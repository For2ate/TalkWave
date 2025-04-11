import { useParams } from "react-router-dom";
import styles from "./ChatWindow.module.css";
import { InputBox } from "./InputBox";
import { useChatHub } from "Features/Chat/Model/Hooks/UseChatHub";
import { useEffect } from "react";
import { useAppDispatch } from "Shared/Lib/hooks";
import { Messages } from "./Messages/Messages";

export const ChatWindow = () => {
  const { chatId } = useParams<{ chatId: string }>();
  const dispatch = useAppDispatch();
  const hub = useChatHub();

  const RecieveMessage = () => {};

  useEffect(() => {
    hub?.on(`ReceiveMessage`, RecieveMessage);
  }, [hub]);

  const handleSendMessage = (message: string) => {
    const request = {
      senderId: localStorage["userId"],
      chatId: chatId,
      content: message,
    };
    hub?.invoke(`SendMessage`, request);
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
