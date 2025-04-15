import axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { ChatApiUrl } from "./Constants";

const instance: AxiosInstance = axios.create({
    baseURL: ChatApiUrl,
    timeout: 10000,
    headers: {
      'Content-Type': 'application/json'
    }
});

export const ChatApi = {

    get: <T>(url: string, config?: AxiosRequestConfig) => 
        instance.get<T>(url, config).then(res => res.data),
      
    post: <T>(url: string, data?: unknown, config?: AxiosRequestConfig) => 
        instance.post<T>(url, data, config).then(res => res.data),
      
    put: <T>(url: string, data?: unknown, config?: AxiosRequestConfig) => 
        instance.put<T>(url, data, config).then(res => res.data),
    
    delete: <T>(url: string, config?: AxiosRequestConfig) => 
        instance.delete<T>(url, config).then(res => res.data)

}