import { Injectable } from '@angular/core';
import { NotificationsService } from '@shared/services/notifications.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class HttpErrorHandlerService {
	constructor(private notificationService: NotificationsService) {}

	handleError<T>(operation = 'operation', result?: T) {
		return (error: HttpErrorResponse): Observable<T> => {
			console.log(error.url);
			console.log(error);
			this.notificationService.add(`Error en: ${operation}`, 'Ok');
			return of(result as T);
		};
	}
}
