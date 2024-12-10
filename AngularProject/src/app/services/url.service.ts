import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { UrlTableEntry } from '../interfaces/url-table-entry';
import { map, Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  api = environment.apiUrl;
  urlTable: UrlTableEntry[] = [];

  constructor(private http: HttpClient, private auth: AuthService) {
    
   }

  goToInfo(url: string) {
  }

  getAll(): Observable<UrlTableEntry[]> {
    return this.http.get<UrlTableEntry[]>(`${this.api}/Urls/All`)
      .pipe(map((response: UrlTableEntry[]) => {
        return response.map(entry => {
          return {
            id: entry.id,
            userId: entry.userId,
            originalUrlString: entry.originalUrlString,
            shortUrlString: entry.shortUrlString
          }
        });
      }));
  }

  getPaginated(page: number = 1, limit: number = 10): Observable<UrlTableEntry[]> {
    return this.http.get<UrlTableEntry[]>(`${this.api}/urls?page=${page}&limit=${limit}`)
      .pipe(map((response: UrlTableEntry[]) => {     
        return response.map(entry => {     
          return {
            id: entry.id,
            userId: entry.userId,
            originalUrlString: entry.originalUrlString,
            shortUrlString: entry.shortUrlString
          }
        }
      );
    }));
  }

  create(url: string): Observable<UrlTableEntry> {
    const request = { originalUrl: url };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    
    return this.http.post<UrlTableEntry>(`${this.api}/Urls`, request, { headers })
    .pipe(map((response: UrlTableEntry) => {
      return {
        id: response.id,
        userId: response.userId,
        originalUrlString: response.originalUrlString,
        shortUrlString: response.shortUrlString
      }
    }));
  }

  delete(shortUrl: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/Urls/${shortUrl}`);
  }

  deleteAll(): Observable<void> {
    return this.http.delete<void>(`${this.api}/Urls`);
  }
}
