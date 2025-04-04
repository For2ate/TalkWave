import {
  CloseSidebarIcon,
  IconButton,
  SearchIcon,
  StyledInput,
} from "Shared/Ui";

import { useSidebar } from "App/Providers";

import styles from "../Styles/OpenSidebar.module.css";
import { ChatList } from "Features/Chat/UI/ChatList/ChatList";

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
        <div className={styles.searchBox}>
          <p>
            <StyledInput label="" onChange={() => {}} value="search" />
          </p>
          <IconButton size={35}>
            <SearchIcon color={"white"} />
          </IconButton>
        </div>
      </header>
      <main>
        <ChatList></ChatList>
      </main>
    </div>
  );
};
