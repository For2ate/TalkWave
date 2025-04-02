import {
  IconButton,
  OpenSidebarIcon,
  CloseSidebarIcon,
  StyledInput,
  SearchIcon,
} from "Shared/Ui";
import { useSidebarContext } from "../../Contexts";

import styles from "./Sidebar.module.css";
import closedStyles from "./CloseSidebar.module.css";
import openStyles from "./OpenSidebar.module.css";
import { ChatSidebar } from "../Chats/ChatSidebar";
import { Chats } from "../Chats/Chats";

export const Sidebar = () => {
  const { isOpen } = useSidebarContext();

  return (
    <div className={styles.layout}>
      {isOpen ? <OpenSidebar /> : <ClosedSidebar />}
    </div>
  );
};

const OpenSidebar = () => {
  const { toggleSideBar } = useSidebarContext();
  return (
    <div className={openStyles.openLayout}>
      <header className={openStyles.header}>
        <div className={openStyles.topHeader}>
          <h1>TalkWave</h1>
          <IconButton onClick={toggleSideBar} size={35}>
            <CloseSidebarIcon color="white" />
          </IconButton>
        </div>
        <div className={openStyles.searchBox}>
          <p>
            <StyledInput label="" onChange={() => {}} value="search" />
          </p>
          <IconButton size={35}>
            <SearchIcon color={"white"} />
          </IconButton>
        </div>
      </header>
      <main>
        <Chats></Chats>
      </main>
    </div>
  );
};

const ClosedSidebar = () => {
  const { toggleSideBar } = useSidebarContext();

  return (
    <div className={closedStyles.closedLayout}>
      <div className={closedStyles.header}>
        <IconButton onClick={toggleSideBar} size={35}>
          <OpenSidebarIcon color="white" />
        </IconButton>
      </div>
    </div>
  );
};
