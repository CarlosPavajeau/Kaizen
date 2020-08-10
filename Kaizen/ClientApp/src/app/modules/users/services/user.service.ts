import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '@app/core/models/user';
import { AUTH_API_URL } from '@app/global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {}

  confirmEmail(token: string, email: string): Observable<User> {
    return this.http.post<User>(`${AUTH_API_URL}/ConfirmEmail?token=${token}&email=${email}`, { token, email });
  }
}
