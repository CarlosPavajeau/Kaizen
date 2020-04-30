import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
	providedIn: 'root'
})
export class NotificationsService {
	constructor(private snackBar: MatSnackBar, private zone: NgZone) {}

	showNotification(message: string, action: string, error: boolean) {
		this.zone.run(() => {
			this.snackBar.open(message, action);
		});
	}
}
