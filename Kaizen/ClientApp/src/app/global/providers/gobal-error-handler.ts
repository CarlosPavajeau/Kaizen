import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from '@shared/services/notifications.service';

@Injectable({
	providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {
	constructor(private notificationsService: NotificationsService) {}

	handleError(error: Error | HttpErrorResponse): void {
		console.log(error);
		if (error instanceof HttpErrorResponse) {
			console.log(error.error.Message);
			if (error.error.Message) {
				this.notificationsService.addMessage(error.error.Message, 'Ok');
			} else if (error.error.errors) {
				for (const prop in error.error.errors) {
					error.error.errors[prop].forEach((element: string) => {
						this.notificationsService.addMessage(element, 'Ok');
					});
				}
			}
		}
	}
}
