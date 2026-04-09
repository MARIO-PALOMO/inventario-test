
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private url = environment.api.url;

  constructor(private http: HttpClient) {}

  get<T>(endpoint: string, options: object = {}): Observable<T> {
    return this.http.get<T>(`${this.url}/${endpoint}`, options);
  }

  post<T>(endpoint: string, body: any, options: object = {}): Observable<T> {
    return this.http.post<T>(`${this.url}/${endpoint}`, body, options);
  }

  put<T>(endpoint: string, body: any, options: object = {}): Observable<T> {
    return this.http.put<T>(`${this.url}/${endpoint}`, body, options);
  }

  patch<T>(endpoint: string, body: any, options: object = {}): Observable<T> {
    return this.http.patch<T>(`${this.url}/${endpoint}`, body, options);
  }

  delete<T>(endpoint: string, options: object = {}): Observable<T> {
    return this.http.delete<T>(`${this.url}/${endpoint}`, options);
  }
}
