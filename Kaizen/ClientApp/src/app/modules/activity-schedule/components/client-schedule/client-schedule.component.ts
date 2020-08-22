import { Component, OnInit } from '@angular/core';
import { Client } from '@app/modules/clients/models/client';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';

@Component({
  selector: 'app-client-schedule',
  templateUrl: './client-schedule.component.html',
  styleUrls: [ './client-schedule.component.css' ]
})
export class ClientScheduleComponent implements OnInit {
  client: Client;
  pendingActivities: Activity[];
  date: Date = new Date();

  constructor(private activityScheduleService: ActivityScheduleService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.client = JSON.parse(localStorage.getItem('current_person'));

    this.activityScheduleService.getPendingClientActivities(this.client.id).subscribe((pendingActivities) => {
      this.pendingActivities = pendingActivities;
    });
  }
}
