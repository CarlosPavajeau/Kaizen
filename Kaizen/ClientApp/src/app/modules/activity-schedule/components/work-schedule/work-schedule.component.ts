import { Component, OnInit } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Employee } from '@modules/employees/models/employee';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-work-schedule',
  templateUrl: './work-schedule.component.html'
})
export class WorkScheduleComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  employee: Employee;
  pendingActivities$: Observable<Activity[]>;

  constructor(private activityScheduleService: ActivityScheduleService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.employee = JSON.parse(localStorage.getItem('current_person'));
    this.pendingActivities$ = this.activityScheduleService.getPendingEmployeeActivities(this.employee.id);
  }
}
