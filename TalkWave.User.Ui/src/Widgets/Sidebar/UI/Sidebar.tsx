import styles from "../Styles/Sidebar.module.css";
import { useSidebar } from "App/Providers";
import { ClosedSidebar } from "./CloseSidebar";
import { OpenSidebar } from "./OpenSidebar";

export const Sidebar = () => {
  const { isOpen } = useSidebar();

  return (
    <div className={styles.layout}>
      {isOpen ? <OpenSidebar /> : <ClosedSidebar />}
    </div>
  );
};
