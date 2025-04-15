import axios from "axios";
import { LoginData, RegisterData } from "Entities/User/SignInModels";
import { UserFullResponse } from "Entities/User/UserModels";
import { UserApiUrl } from "Shared/Api/Constants";


export const Login = async (
    data: LoginData
  ): Promise<UserFullResponse | null> => {
    try {
      const result = await axios.post(`${UserApiUrl}/Api/Auth/Login`, data);
      return result.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error login user:", error.response?.data || error.message);
      } else {
        console.error("Unexpected error:", error);
      }
      return null;
    }
  };

  export const Register = async (data: RegisterData) => {
    try {
      const result = await axios.post(`${UserApiUrl}/Api/Auth/Register`, data);
      return { success: true, data: result.data };
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const errorData = error.response?.data;
        let firstWordData = "";
        if (
          errorData === "Login is already taken." ||
          errorData === "Email is already taken."
        ) {
          firstWordData = errorData.split(" ")[0];
        }
        const errors = errorData?.errors || {
          [firstWordData]: [errorData],
        };
        return { success: false, errors };
      } else {
        console.error("Unexpected error:", error);
        return {
          success: false,
          errors: { General: ["An unexpected error occurred."] },
        };
      }
    }
  };