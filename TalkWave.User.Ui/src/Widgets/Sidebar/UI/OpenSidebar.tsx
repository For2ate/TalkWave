import { CloseSidebarIcon, IconButton } from "Shared/Ui";

import { useSidebar } from "App/Providers";

import styles from "../Styles/OpenSidebar.module.css";
import { ChatList } from "Features/Chat/UI/ChatList/ChatList";
import { CreateChatButton, Search } from "Features/Chat/UI/ChatList";

export const OpenSidebar = () => {
  const { toggleSideBar } = useSidebar();
  return (
    <div className={styles.openLayout}>
      <header className={styles.header}>
        <div className={styles.topHeader}>
          <h1>TalkWave</h1>
          <IconButton onClick={toggleSideBar} size={35}>
            <CloseSidebarIcon color="white" />
          </IconButton>
        </div>
        <Search />
        <CreateChatButton />
      </header>
      <main>
        <ChatList></ChatList>
      </main>
    </div>
  );
};
