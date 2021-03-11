import { Injectable, NgZone, OnDestroy } from '@angular/core';
import {
  MatSnackBarRef,
  SimpleSnackBar,
  MatSnackBar,
  MatSnackBarHorizontalPosition
} from '@angular/material/snack-bar';
import { SnackBarMessage } from '@shared/models/snackbar-message';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService implements OnDestroy {
  private messageQueue: SnackBarMessage[] = [];
  private subscription: Subscription;
  private snackBarRef: MatSnackBarRef<SimpleSnackBar>;
  private isInstanceVisible = false;

  constructor(private zone: NgZone, private snackBar: MatSnackBar) {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  addMessage(message: string, action?: string, horizontalPosition?: MatSnackBarHorizontalPosition) {
    const snack_message: SnackBarMessage = {
      message: message,
      action: action || 'Ok',
      config: {
        horizontalPosition: horizontalPosition || 'center',
        verticalPosition: 'bottom'
      }
    };
    this.messageQueue.push(snack_message);

    if (!this.isInstanceVisible) {
      this.showNext();
    }
  }

  private showNext() {
    if (this.messageQueue.length === 0) {
      return;
    }

    const message = this.messageQueue.shift();
    this.isInstanceVisible = true;

    this.snackBarRef = this.showNotification(message);

    this.snackBarRef.afterDismissed().subscribe(() => {
      this.isInstanceVisible = false;
      this.showNext();
    });
  }

  private showNotification(message: SnackBarMessage) {
    return this.zone.run(() => {
      return this.snackBar.open(message.message, message.action, message.config);
    });
  }
}
