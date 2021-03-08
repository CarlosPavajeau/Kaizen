import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '@core/models/login-request';
import { User } from '@core/models/user';
import { AUTH_API_URL } from '@global/endpoints';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private readonly userLocalStorageKey = 'currentUser';
  private readonly userTokenKey = 'user_token';

  constructor(private http: HttpClient, private cookieService: CookieService) {
  }

  registerUser(user: User): Observable<User> {
    return this.http.post<User>(AUTH_API_URL, user);
  }

  loginUser(user: LoginRequest): Observable<User> {
    return this.http.post<User>(`${ AUTH_API_URL }/Login`, user);
  }

  logoutUser(): Observable<boolean> {
    this.removeUser();
    localStorage.removeItem('current_person');

    return this.http.post<boolean>(`${ AUTH_API_URL }/Logout`, null);
  }

  removeUser(): void {
    localStorage.removeItem(this.userLocalStorageKey);
    this.cookieService.delete(this.userTokenKey);
  }

  setCurrentUser(user: User): void {
    this.cookieService.set(this.userTokenKey, user.token, 365, '/', null, true);
    user.token = undefined;
    const user_str = JSON.stringify(user);
    localStorage.setItem(this.userLocalStorageKey, user_str);
  }

  getToken(): string {
    return this.cookieService.get(this.userTokenKey);
  }

  getCurrentUser(): User {
    const userToken = this.getToken();
    if (userToken === null || userToken === undefined || userToken === '') {
      return null;
    }

    const user_str = localStorage.getItem(this.userLocalStorageKey);
    if (user_str === null || user_str === undefined) {
      return null;
    }

    return JSON.parse(user_str) as User;
  }

  getUserRole(): string {
    const payload = JSON.parse(window.atob(this.getToken().split('.')[1]));
    return payload.role;
  }

  userLoggedIn(): boolean {
    return this.getCurrentUser() != null;
  }
}
