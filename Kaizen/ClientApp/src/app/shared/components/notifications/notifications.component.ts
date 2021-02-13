import { Component, OnDestroy, OnInit } from '@angular/core';
import { ClientSignalrService } from '@modules/clients/services/client-signalr.service';
import { NotificationItem } from '@shared/models/notification-item';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.scss' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: NotificationItem[] = [];

  constructor(private clientSignalR: ClientSignalrService) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {}
}
