import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { NOTIFICATIONS_API_URL } from '@global/endpoints';
import { NotificationItem } from '@shared/models/notification-item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http: HttpClient, private authService: AuthenticationService) {
  }

  getNotifications(userId: string): Observable<NotificationItem[]> {
    return this.http.get<NotificationItem[]>(`${NOTIFICATIONS_API_URL}/${userId}`);
  }
}
