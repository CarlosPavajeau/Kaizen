import { ErrorDialogComponent } from '../components/error-dialog/error-dialog.component';
import { Injectable, NgZone, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
	MatSnackBar,
	MatSnackBarHorizontalPosition,
	MatSnackBarRef,
	SimpleSnackBar
} from '@angular/material/snack-bar';
import { SnackBarMessage } from '@shared/models/snackbar-message';
import { Subscription } from 'rxjs';
import { SuccessDialogComponent } from '../components/success-dialog/success-dialog.component';

@Injectable({
	providedIn: 'root'
})
export class NotificationsService implements OnDestroy {
	private messageQueue: SnackBarMessage[] = [];
	private subscription: Subscription;
	private snackBarRef: MatSnackBarRef<SimpleSnackBar>;
	private isInstanceVisible = false;

	constructor(private snackBar: MatSnackBar, private zone: NgZone, private dialog: MatDialog) {}

	ngOnDestroy(): void {
		this.subscription.unsubscribe();
	}

	showSuccessMessage(message: string, onClose: () => void): void {
		this.zone.run(() => {
			const dialogRef = this.dialog.open(SuccessDialogComponent, {
				width: '500px',
				data: message
			});

			dialogRef.afterClosed().subscribe(() => {
				onClose();
			});
		});
	}

	showErrorMessage(message: string, onClose?: () => void): void {
		this.zone.run(() => {
			const dialogRef = this.dialog.open(ErrorDialogComponent, {
				width: '500px',
				data: message
			});

			dialogRef.afterClosed().subscribe(() => {
				if (onClose) {
					onClose();
				}
			});
		});
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
