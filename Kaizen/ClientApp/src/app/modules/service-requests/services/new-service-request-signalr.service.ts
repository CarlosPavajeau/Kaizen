import { Injectable } from '@angular/core';
import { BaseSignalrService } from '@app/core/services/base-signalr.service';
import { AuthenticationService } from '@app/core/authentication/authentication.service';
import { ServiceRequest } from '../models/service-request';

@Injectable({
	providedIn: 'root'
})
export class NewServiceRequestSignalrService extends BaseSignalrService<ServiceRequest> {
	constructor(private authService: AuthenticationService) {
		super('/ServiceRequestsHub', 'NewServiceRequest');
		this.ngOnInit();
	}

	ngOnInit(): void {
		this.token = this.authService.getToken();
		super.ngOnInit();
	}
}
