import { useParams } from "react-router-dom";
import styles from "./ChatWindow.module.css";

export const ChatWindow = () => {
  const { chatId } = useParams<{ chatId: string }>();

  return (
    <div className={styles.layout}>
      <h1>Чат {chatId}</h1>
    </div>
  );
};
