import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { tap, catchError } from "rxjs/operators";
import { Endpoints } from 'src/app/global/endpoints';
import { isNullOrUndefined } from 'util';
import { LoginRequest } from '../models/login-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  readonly USER_LOCALSTORAGE_KEY = 'currentUser';

  constructor(
    private http: HttpClient
  ) { }

  registerUser(user: User): Observable<User> {
    return this.http.post<User>(Endpoints.AuthUrl, user)
      .pipe(
        tap(_ => console.log('Msg'))
      )
  }

  loginUser(user: LoginRequest): Observable<User> {
    return this.http.post<User>(`${Endpoints.AuthUrl}/Login`, user)
      .pipe(
        tap(_ => console.log('Msg'))
      )
  }

  logoutUser(): void {
    this.removeUser();
  }

  removeUser(): void {
    localStorage.removeItem(this.USER_LOCALSTORAGE_KEY);
  }

  setCurrentUser(user: User): void {
    let user_str = JSON.stringify(user);
    localStorage.setItem(this.USER_LOCALSTORAGE_KEY, user_str);
  }

  getToken(): string {
    const user: User = this.getCurrentUser();
    if (user) {
      return user.token;
    } else {
      return '';
    }
  }

  getCurrentUser(): User {
    let user_str = localStorage.getItem(this.USER_LOCALSTORAGE_KEY);
    if (isNullOrUndefined(user_str)) {
      return null;
    } else {
      const user: User = JSON.parse(user_str);
      return user;
    }
  }

  userLoggedIn(): boolean {
    return this.getCurrentUser() != null;
  }
}
