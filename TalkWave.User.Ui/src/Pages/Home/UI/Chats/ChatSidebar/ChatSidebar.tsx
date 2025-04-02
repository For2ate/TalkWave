import styles from "./ChatSidebar.module.css";

interface Props {
  chat: string;
}

export const ChatSidebar = ({ chat }: Props) => {
  return (
    <div className={styles.layout}>
      <button className={styles.chatButton}>
        <h2>{chat}</h2>
      </button>
    </div>
  );
};
