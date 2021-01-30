import { Component, OnDestroy, OnInit } from '@angular/core';
import { NewClientSignalrService } from '@modules/clients/services/new-client-signalr.service';
import { NotificationItem } from '@shared/models/notification-item';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.scss' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: NotificationItem[] = [];

  constructor(private clientSignalR: NewClientSignalrService) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {}
}
