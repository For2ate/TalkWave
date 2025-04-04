import { useSidebar } from "App/Providers";
import { IconButton, OpenSidebarIcon } from "Shared/Ui";

import styles from "../Styles/CloseSidebar.module.css";

export const ClosedSidebar = () => {
  const { toggleSideBar } = useSidebar();

  return (
    <div className={styles.closedLayout}>
      <div className={styles.header}>
        <IconButton onClick={toggleSideBar} size={35}>
          <OpenSidebarIcon color="white" />
        </IconButton>
      </div>
    </div>
  );
};
