export enum NotificationState {
  Pending,
  View
}

export interface NotificationItem {
  id: number;
  icon: string;
  title: string;
  message: string;
  state: NotificationState;
  url?: string;
}
