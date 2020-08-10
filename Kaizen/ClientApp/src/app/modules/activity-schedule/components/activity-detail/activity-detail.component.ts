import { Component, OnInit } from '@angular/core';
import { ActivityScheduleService } from '../../services/activity-schedule.service';
import { Activity } from '../../models/activity';
import { ActivatedRoute } from '@angular/router';

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
