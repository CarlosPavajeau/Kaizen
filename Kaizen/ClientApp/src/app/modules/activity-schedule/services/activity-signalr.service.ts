import { EventEmitter, Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { Activity } from '@modules/activity-schedule/models/activity';

@Injectable({
  providedIn: 'root'
})
export class ActivitySignalrService extends BaseSignalrService {
  onNewActivityRegister: EventEmitter<Activity> = new EventEmitter<Activity>();

  constructor(private authService: AuthenticationService) {
    super();
  }

  public startConnection() {
    super.buildConnection('/ActivityHub', this.authService.getToken());
    super.startConnection();
  }

  public addOnNewActivityRegister(): void {
    this.hubConnection.on('NewActivity', (activity: Activity) => {
      this.onNewActivityRegister.emit(activity);
    });
  }
}
