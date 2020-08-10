import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

import { AUTH_API_URL } from '@global/endpoints';
import { LoginRequest } from '@core/models/login-request';
import { User } from '@core/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  readonly USER_LOCALSTORAGE_KEY = 'currentUser';

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  registerUser(user: User): Observable<User> {
    return this.http.post<User>(AUTH_API_URL, user);
  }

  loginUser(user: LoginRequest): Observable<User> {
    return this.http.post<User>(`${AUTH_API_URL}/Login`, user);
  }

  logoutUser(): void {
    this.removeUser();
  }

  removeUser(): void {
    localStorage.removeItem(this.USER_LOCALSTORAGE_KEY);
    this.cookieService.delete('user_token');
  }

  setCurrentUser(user: User): void {
    this.cookieService.set('user_token', user.token, 365, '/', null, true);
    user.token = undefined;
    const user_str = JSON.stringify(user);
    localStorage.setItem(this.USER_LOCALSTORAGE_KEY, user_str);
  }

  getToken(): string {
    return this.cookieService.get('user_token');
  }

  getCurrentUser(): User {
    const user_str = localStorage.getItem(this.USER_LOCALSTORAGE_KEY);
    if (user_str === null || user_str === undefined) {
      return null;
    } else {
      const user: User = JSON.parse(user_str);
      return user;
    }
  }

  getUserRole(): string {
    const payload = JSON.parse(window.atob(this.getToken().split('.')[1]));
    return payload.role;
  }

  userLoggedIn(): boolean {
    return this.getCurrentUser() != null;
  }
}
