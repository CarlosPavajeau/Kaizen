import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthenticationService } from '@core/authentication/authentication.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
	constructor(private authService: AuthenticationService, private router: Router) {}

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(request).pipe(
			catchError((error: HttpErrorResponse) => {
				if ([ 401, 403 ].includes(error.status)) {
					this.authService.logoutUser();
          window.location.reload();
				}
				return throwError(error);
			})
		);
	}
}
