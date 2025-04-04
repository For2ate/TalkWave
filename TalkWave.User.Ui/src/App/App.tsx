import { AppRouter } from "./Routes";
import { StoreProvider } from "./Store/StoreProvider";

import "./Styles/global.css";

const App = () => {
  return (
    <StoreProvider>
      <AppRouter />
    </StoreProvider>
  );
};

export default App;
