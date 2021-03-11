import { Component, OnDestroy, OnInit } from '@angular/core';
import { NotificationItem } from '@shared/models/notification-item';
import { NotificationsSignalrService } from '@shared/services/notifications-signalr.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.scss' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: NotificationItem[] = [];
  newNotification: Subscription;

  constructor(private notificationSignalR: NotificationsSignalrService) {
  }

  ngOnInit(): void {
    this.notificationSignalR.startConnection();
    this.notificationSignalR.addOnNewNotification();

    this.newNotification = this.notificationSignalR.onNewNotification$.subscribe((notification: NotificationItem) => {
      this.notifications.push(notification);
    });
  }

  ngOnDestroy(): void {
    this.newNotification.unsubscribe();
  }
}
