import { createBrowserRouter, createHashRouter } from "react-router";
import { HomePage } from "Pages/Home";
import { LoginPage, RegisterPage } from "Pages/Sign-In";
import { RouterProvider } from "react-router-dom";
import { ChatWindow } from "Features/Chat/UI/ChatWindow/ChatWindow";

export const router = createHashRouter([
  {
    path: "/",
    element: <HomePage />,
    children: [
      {
        path: "chats/:chatId",
        element: <ChatWindow />,
      },
    ],
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
]);

export const AppRouter = () => {
  return <RouterProvider router={router}></RouterProvider>;
};
