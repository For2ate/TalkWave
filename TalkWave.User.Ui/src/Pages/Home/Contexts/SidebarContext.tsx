import { createContext, useContext, useState } from "react";

interface SidebarContextType {
  isOpen: boolean;
  toggleSideBar: () => void;
}

const SidebarContext = createContext<SidebarContextType>({
  isOpen: false,
  toggleSideBar: () => {},
});

export const SidebarProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [isOpen, setIsOpen] = useState(true);

  const toggleSideBar = () => {
    setIsOpen((prev) => !prev);
  };

  return (
    <SidebarContext.Provider value={{ isOpen, toggleSideBar }}>
      {children}
    </SidebarContext.Provider>
  );
};

export const useSidebarContext = () => useContext(SidebarContext);
