import { createBrowserRouter } from "react-router";
import { HomePage } from "Pages/Home";
import { LoginPage, RegisterPage } from "Pages/Sign-In";
import { RouterProvider } from "react-router-dom";
import { ChatWindow } from "Features/Chat/UI/ChatWindow/ChatWindow";

export const router = createBrowserRouter([
  {
    path: "/",
    Component: HomePage,
    children: [
      {
        path: "chats/:chatId",
        element: <ChatWindow />,
      },
    ],
  },
  {
    path: "/login",
    Component: LoginPage,
  },
  {
    path: "/register",
    Component: RegisterPage,
  },
]);

export const AppRouter = () => {
  return <RouterProvider router={router}></RouterProvider>;
};
