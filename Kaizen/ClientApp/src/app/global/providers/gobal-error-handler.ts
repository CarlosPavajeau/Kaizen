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
      if (error.status == 401 || error.status == 400) {
        this.notificationsService.showNotification("Error: usted no tiene autorizacion", "Ok", true);
      }
    }
  }

}
