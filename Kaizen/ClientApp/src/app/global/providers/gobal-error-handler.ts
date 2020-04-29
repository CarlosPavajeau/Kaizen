import { ErrorHandler, Injector, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from '@shared/services/notifications.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

  constructor (
    private injector: Injector,
    private notificationsService: NotificationsService
    ) { }

  handleError(error: Error | HttpErrorResponse): void {
    console.log(error);
    if (error instanceof HttpErrorResponse) {
      if (error.error?.ErrorMessage) {
        this.notificationsService.showNotification(error.error.ErrorMessage[0], "Ok", true);
      }
      else {
        this.notificationsService.showNotification(error.error, "Ok", true);
      }
    }
  }

}
