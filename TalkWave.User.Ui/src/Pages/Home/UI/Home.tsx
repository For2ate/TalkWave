import styles from "./Home.module.css";
import { Outlet } from "react-router-dom";
import { SidebarProvider } from "App/Providers";
import { Sidebar } from "Widgets";
import { chatHubService } from "Features/Lib/SignalR";
import { ChatApiUrl } from "Shared/Api/Constants";
import { useChatHub } from "Features/Chat/Model/Hooks/UseChatHub";
import { useEffect } from "react";

await chatHubService.connect(`${ChatApiUrl}/ChatHub`);

export const HomePage = () => {
  const hub = useChatHub();

  useEffect(() => {
    const joinHub = async () => {
      const userId = localStorage["userId"];
      await hub?.invoke("JoinHub", userId);
    };

    if (hub) {
      joinHub();
    }
  }, [hub]);

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
