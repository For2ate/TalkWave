import { createBrowserRouter } from "react-router";
import { HomePage } from "Pages/Home";
import { LoginPage, RegisterPage } from "Pages/Sign-In";
import { RouterProvider } from "react-router-dom";

export const router = createBrowserRouter([
  {
    path: "/",
    Component: HomePage,
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
