import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';

@Component({
  selector: 'app-activity-detail',
  templateUrl: './activity-detail.component.html',
  styleUrls: [ './activity-detail.component.css' ]
})
export class ActivityDetailComponent implements OnInit {
  activity: Activity;

  constructor(private activityService: ActivityScheduleService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    const code = +this.activatedRoute.snapshot.paramMap.get('code');
    this.activityService.getActivity(code).subscribe((activity) => {
      this.activity = activity;
    });
  }
}
