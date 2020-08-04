import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from '@shared/services/notifications.service';
import { Router } from '@angular/router';

@Injectable({
	providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {
	constructor(private notificationsService: NotificationsService, private router: Router) {}

	handleError(error: HttpErrorResponse): void {
		console.log(error);
		if (typeof error.error === 'string') {
			this.notificationsService.showErrorMessage(error.error, () => {
				this.redirectToProfile();
			});
		} else if (error.error.Message) {
			this.notificationsService.showErrorMessage(error.error.Message, () => {
				this.redirectToProfile();
			});
		} else if (error.error.errors) {
			let errorMessage = 'Se presentaron los siguientes errores: <br/>';
			for (const prop of Object.keys(error.error.errors)) {
				error.error.errors[prop].forEach((element: string) => {
					errorMessage += `- ${element} <br/>`;
				});
			}
			this.notificationsService.showErrorMessage(errorMessage, () => {
				this.redirectToProfile();
			});
		}
	}

	redirectToProfile(): void {
		this.router.navigateByUrl('/user/profile');
	}
}
