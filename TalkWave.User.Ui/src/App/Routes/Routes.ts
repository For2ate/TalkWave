import { createBrowserRouter } from 'react-router';
import { HomePage } from 'Pages/Home';
import { LoginPage, RegisterPage } from 'Pages/Sign-In';

export const router = createBrowserRouter([
  {
    path:"/",
    Component: HomePage
  }, 
  {
    path:"/login",
    Component: LoginPage
  },
  {
    path:"/register",
    Component: RegisterPage
  }
])
