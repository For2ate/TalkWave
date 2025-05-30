import { ReactNode } from "react";
import { Provider } from "react-redux";
import { store } from "./MainStore";

export const StoreProvider = ({ children }: { children: ReactNode }) => {
  return <Provider store={store}>{children}</Provider>;
};
