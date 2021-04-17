import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NOTIFICATIONS_API_URL } from '@global/endpoints';
import { NotificationItem } from '@shared/models/notification-item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http: HttpClient) {
  }

  getNotifications(userId: string): Observable<NotificationItem[]> {
    return this.http.get<NotificationItem[]>(`${ NOTIFICATIONS_API_URL }/${ userId }`);
  }

  updateNotification(notification: NotificationItem): Observable<NotificationItem> {
    return this.http.put<NotificationItem>(`${ NOTIFICATIONS_API_URL }/${ notification.id }`, notification);
  }
}
