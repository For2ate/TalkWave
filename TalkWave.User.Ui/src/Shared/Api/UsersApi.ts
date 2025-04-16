import axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { UserApiUrl } from "./Constants";
import { UserFullResponse } from "Entities/User/UserModels";

const instance: AxiosInstance = axios.create({
    baseURL: UserApiUrl,
    timeout: 10000,
    headers: {
      'Content-Type': 'application/json'
    }
});

export const UserApi = {

    get: <T>(url: string, config?: AxiosRequestConfig) => 
        instance.get<T>(url, config).then(res => res.data),
      
    post: <T>(url: string, data?: unknown, config?: AxiosRequestConfig) => 
        instance.post<T>(url, data, config).then(res => res.data),
      
    put: <T>(url: string, data?: unknown, config?: AxiosRequestConfig) => 
        instance.put<T>(url, data, config).then(res => res.data),
    
    delete: <T>(url: string, config?: AxiosRequestConfig) => 
        instance.delete<T>(url, config).then(res => res.data)

}

export const UserApiEndpoints = {

    getUserById: async(id:string) : Promise<UserFullResponse | null> => {

        try {

            return await UserApi.get<UserFullResponse>(`Api/User/${id}`);

        } catch (error) {

            console.error(error);

            return null;

        }

    },

    getUserByEmail: async(email: string): Promise<UserFullResponse | null> => {

        try {

            const params = new URLSearchParams();

            params.append('email',email);

            return await UserApi.get<UserFullResponse>(`Api/User/GetByEmail?${params.toString()}`)

        } catch(error) {

            return null;

        }

    }

}