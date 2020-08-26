import { Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { Activity } from '@modules/activity-schedule/models/activity';

@Injectable({
  providedIn: 'root'
})
export class NewActivitySignalrService extends BaseSignalrService<Activity> {
  constructor(authService: AuthenticationService) {
    super(authService, '/ActivityHub', 'NewActivity');
  }
}
