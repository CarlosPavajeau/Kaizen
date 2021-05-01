import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-activity-detail',
  templateUrl: './activity-detail.component.html'
})
export class ActivityDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  activity$: Observable<Activity>;

  constructor(private activityService: ActivityScheduleService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    const code = +this.activatedRoute.snapshot.paramMap.get('code');
    this.activity$ = this.activityService.getActivity(code);
  }
}
