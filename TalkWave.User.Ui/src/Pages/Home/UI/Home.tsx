import styles from "./Home.module.css";
import { Outlet } from "react-router-dom";
import { SidebarProvider } from "App/Providers";
import { Sidebar } from "Widgets";

export const HomePage = () => {
  return (
    <div className={styles.layout}>
      <div className={styles.layoutContainer}>
        <SidebarProvider>
          <Sidebar></Sidebar>
        </SidebarProvider>
        <Outlet />
      </div>
    </div>
  );
};
