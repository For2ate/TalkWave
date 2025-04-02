import styles from "./Home.module.css";
import { SidebarProvider } from "../Contexts";
import { Sidebar } from "./Sidebar";
import { Chat } from "./Chats/Chat";

export const HomePage = () => {
  return (
    <div className={styles.layout}>
      <div className={styles.layoutContainer}>
        <SidebarProvider>
          <Sidebar></Sidebar>
        </SidebarProvider>
        <Chat></Chat>
      </div>
    </div>
  );
};
