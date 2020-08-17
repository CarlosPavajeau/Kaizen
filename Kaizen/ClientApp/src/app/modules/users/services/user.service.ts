import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/core/models/user';
import { AUTH_API_URL } from '@app/global/endpoints';
import { Observable } from 'rxjs';
import { ChangePasswordModel } from '../models/change-password';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {}

  confirmEmail(token: string, email: string): Observable<User> {
    return this.http.post<User>(`${AUTH_API_URL}/ConfirmEmail?token=${token}&email=${email}`, { token, email });
  }

  changePassword(id: string, changePasswordModel: ChangePasswordModel): Observable<User> {
    return this.http.put<User>(`${AUTH_API_URL}/ChangePassword/${id}`, changePasswordModel);
  }
}
