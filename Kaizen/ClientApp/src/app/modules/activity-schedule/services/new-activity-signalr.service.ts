import { Injectable, OnInit } from '@angular/core';
import { BaseSignalrService } from '@app/core/services/base-signalr.service';
import { Activity } from '../models/activity';
import { AuthenticationService } from '@core/authentication/authentication.service';

@Injectable({
	providedIn: 'root'
})
export class NewActivitySignalrService extends BaseSignalrService<Activity> implements OnInit {
	constructor(private authService: AuthenticationService) {
		super('/ActivityHub', 'NewActivity');
		this.ngOnInit();
	}

	ngOnInit(): void {
		this.token = this.authService.getToken();
		super.ngOnInit();
	}
}
