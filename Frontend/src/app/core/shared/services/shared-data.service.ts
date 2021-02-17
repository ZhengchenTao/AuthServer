import { AxiosRequestConfig } from 'axios';
import { throwError, Observable } from 'rxjs';

class SharedDataService {
  get(selector: string, endpoint: string, config?: AxiosRequestConfig, id?: string, url?: string): Observable<any> {
    return throwError(new Error('[Shared-Data Service] Illegal selector/endpoint combination.'));
  }

  post(selector: string, endpoint: string, data?: any, config?: AxiosRequestConfig): Observable<any> {
    return throwError(new Error('[Shared-Data Service] Illegal selector/endpoint combination.'));
  }

  put(selector: string, endpoint: string, data?: any, config?: AxiosRequestConfig, id?: string): Observable<any> {
    return throwError(new Error('[Shared-Data Service] Illegal selector/endpoint combination.'));
  }
}

export const sharedDataService = new SharedDataService();
