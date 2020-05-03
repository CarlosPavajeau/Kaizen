import { Injectable, NgZone, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, SimpleSnackBar } from '@angular/material/snack-bar';
import { Subject, Subscription } from 'rxjs';
import { SnackBarMessage } from '../models/snackbar-message';

@Injectable({
	providedIn: 'root'
})
export class NotificationsService implements OnDestroy {
	private messageQueue: SnackBarMessage[] = [];
	private subscription: Subscription;
	private snackBarRef: MatSnackBarRef<SimpleSnackBar>;
	private isInstanceVisible = false;

	constructor(private snackBar: MatSnackBar, private zone: NgZone) {}

	ngOnDestroy(): void {
		this.subscription.unsubscribe();
	}

	add(message: string, action?: string) {
		const snack_message: SnackBarMessage = {
			message: message,
			action: action
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

		this.snackBarRef = this.showNotification(message.message, message.action, false);

		this.snackBarRef.afterDismissed().subscribe(() => {
			this.isInstanceVisible = false;
			this.showNext();
		});
	}

	private showNotification(message: string, action: string, error: boolean) {
		return this.zone.run(() => {
			return this.snackBar.open(message, action);
		});
	}
}
