import Axios, { AxiosInstance, AxiosInterceptorManager, AxiosRequestConfig, AxiosResponse } from 'axios';
import { from, Observable } from 'rxjs';

class ObservableAxios {
  static readonly isCancel = Axios.isCancel.bind(Axios);
  static readonly Cancel = Axios.Cancel;
  static readonly CancelToken = Axios.CancelToken;

  interceptors: {
    request: AxiosInterceptorManager<AxiosRequestConfig>;
    response: AxiosInterceptorManager<AxiosResponse>;
  };

  private readonly handler: AxiosInstance;

  constructor(config: AxiosRequestConfig) {
    this.handler = Axios.create(config);
    this.interceptors = this.handler.interceptors;
  }

  request<T = any>(config: AxiosRequestConfig): Observable<AxiosResponse<T>> {
    return from(this.handler.request<T>(config));
  }

  get<T = any>(url: string, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
    return from(this.handler.get<T>(url, config));
  }

  delete(url: string, config?: AxiosRequestConfig): Observable<AxiosResponse> {
    return from(this.handler.delete(url, config));
  }

  head(url: string, config?: AxiosRequestConfig): Observable<AxiosResponse> {
    return from(this.handler.head(url, config));
  }

  post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
    return from(this.handler.post<T>(url, data, config));
  }

  put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
    return from(this.handler.put<T>(url, data, config));
  }

  patch<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
    return from(this.handler.patch<T>(url, data, config));
  }
}

const defaultConfig: AxiosRequestConfig = {
  baseURL: process.env.REACT_APP_API_BASE_URL + '/api'
};

export const axios = new ObservableAxios(defaultConfig);
