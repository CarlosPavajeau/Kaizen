import { Activity } from '../models/activity';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NewActivitySignalrService extends BaseSignalrService<Activity> {
  constructor(authService: AuthenticationService) {
    super(authService, '/ActivityHub', 'NewActivity');
  }
}
