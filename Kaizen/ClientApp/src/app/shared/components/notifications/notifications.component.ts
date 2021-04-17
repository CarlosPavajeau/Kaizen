import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { NotificationItem, NotificationState } from '@shared/models/notification-item';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsSignalrService } from '@shared/services/notifications-signalr.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { Subscription, Observable } from 'rxjs';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.scss' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  notifications$: Observable<NotificationItem[]>;
  notifications: NotificationItem[] = [];
  newNotification: Subscription;

  constructor(
    private notificationsService: NotificationsService,
    private authService: AuthenticationService,
    private notificationSignalR: NotificationsSignalrService
  ) {
  }

  ngOnInit(): void {
    const userId = this.authService.getCurrentUser().id;
    this.notifications$ = this.notificationsService.getNotifications(userId);

    this.notifications$.subscribe((notifications: NotificationItem[]) => {
      this.notifications = notifications;
    });

    this.newNotification = this.notificationSignalR.onNewNotification$.subscribe((notification: NotificationItem) => {
      this.notifications.push(notification);
    });
  }

  ngOnDestroy(): void {
    this.newNotification.unsubscribe();
  }

  onViewOrDeleteNotification(notification: NotificationItem): void {
    notification.state = NotificationState.View;
    this.notificationsService.updateNotification(notification)
      .subscribe((notificationUpdated: NotificationItem) => {
        this.notifications = this.notifications.filter(n => n.id !== notificationUpdated.id);
      });
  }
}
