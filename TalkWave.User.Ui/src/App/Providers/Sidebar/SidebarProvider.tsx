import React, { createContext, useState } from "react";
import { SidebarContextType } from "./Context/Types";

export const SidebarContext = createContext<SidebarContextType>({
  isOpen: false,
  toggleSideBar: () => {},
  openSideBar: () => {},
  closeSideBar: () => {},
});

export const SidebarProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [isOpen, setIsOpen] = useState(true);

  const toggleSideBar = () => setIsOpen((prev) => !prev);
  const openSideBar = () => setIsOpen(true);
  const closeSideBar = () => setIsOpen(false);

  return (
    <SidebarContext.Provider
      value={{
        isOpen,
        toggleSideBar,
        openSideBar,
        closeSideBar,
      }}
    >
      {children}
    </SidebarContext.Provider>
  );
};
