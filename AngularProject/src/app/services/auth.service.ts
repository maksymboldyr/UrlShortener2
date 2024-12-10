import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  api: string;
  http: HttpClient;
  router : Router;
  roles: string[] = [];
  userId: string = '';
  

  constructor(http: HttpClient, router: Router) { 
    this.api = environment.apiUrl;
    this.http = http;
    this.router = router;
    this.getRoles();
    this.getUserId().subscribe((data) => {
      this.userId = data;
    });
  }

  getUserId(): Observable<string> {
    return this.http.get(`${this.api}/rbac/CurrentUserId`, { responseType: 'text' }).pipe(
      map((data: string) => {
        console.log('Got user id', data);
        return data;
      })
    );
  }

  getRoles() {
    this.http.get<string[]>(`${this.api}/rbac/CurrentRoles`).subscribe((data) => {
      console.log('Got roles', data);
      
      this.roles = data;
    }, (error: HttpErrorResponse) => {
      console.error('Failed to get roles', error);
    });
  }

  hasRole(role: string) : boolean {
    if (!this.roles.length) {
      return false;
    }

    if (Array.isArray(this.roles)) {
      return this.roles.includes(role);
    }

    if (this.roles === role) {
      return true;
    }

    return false;
  }  
}
