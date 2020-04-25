import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private snackBar: MatSnackBar) { }

  showNotification(message: string, action: string, error: boolean) {
    this.snackBar.open(message, action);
  }
}
