import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private httpErrorHandler: HttpErrorHandlerService) {}

  handleError(error: HttpErrorResponse): void {
    this.httpErrorHandler.handleHttpError(error);
  }
}
