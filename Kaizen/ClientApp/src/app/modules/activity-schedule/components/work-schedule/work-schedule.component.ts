import { Component, OnInit } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Employee } from '@modules/employees/models/employee';

@Component({
  selector: 'app-work-schedule',
  templateUrl: './work-schedule.component.html',
  styleUrls: [ './work-schedule.component.scss' ]
})
export class WorkScheduleComponent implements OnInit {
  employee: Employee;
  pendingActivities: Activity[];

  constructor(private activityScheduleService: ActivityScheduleService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.employee = JSON.parse(localStorage.getItem('current_person'));

    this.activityScheduleService.getPendingEmployeeActivities(this.employee.id).subscribe((pendingActivities) => {
      this.pendingActivities = pendingActivities;
    });
  }
}
