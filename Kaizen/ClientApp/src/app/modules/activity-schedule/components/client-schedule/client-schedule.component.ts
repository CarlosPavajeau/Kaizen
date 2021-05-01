import { Component, OnInit } from '@angular/core';
import { Client } from '@app/modules/clients/models/client';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-client-schedule',
  templateUrl: './client-schedule.component.html'
})
export class ClientScheduleComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  client: Client;
  pendingActivities$: Observable<Activity[]>;
  date: Date = new Date();

  constructor(private activityScheduleService: ActivityScheduleService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.client = JSON.parse(localStorage.getItem('current_person'));
    this.pendingActivities$ = this.activityScheduleService.getPendingClientActivities(this.client.id);
  }
}
