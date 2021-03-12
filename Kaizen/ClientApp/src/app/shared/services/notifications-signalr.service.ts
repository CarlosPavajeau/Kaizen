import { Injectable, EventEmitter } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { NotificationItem } from '@shared/models/notification-item';

@Injectable({
  providedIn: 'root'
})
export class NotificationsSignalrService extends BaseSignalrService {
  onNewNotification$: EventEmitter<NotificationItem> = new EventEmitter<NotificationItem>();

  constructor(private authService: AuthenticationService) {
    super();
    this.startConnection();
  }

  public startConnection(): void {
    super.buildConnection('/NotificationHub', this.authService.getToken());
    super.startConnection();
    this.addOnNewNotification();
  }

  public addOnNewNotification(): void {
    this.hubConnection.on('OnNewNotification', (notification: NotificationItem) => {
      this.onNewNotification$.emit(notification);
    });
  }
}
