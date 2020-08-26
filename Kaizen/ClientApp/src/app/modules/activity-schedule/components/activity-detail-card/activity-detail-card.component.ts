import { Component, Input, OnInit } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';

@Component({
  selector: 'app-activity-detail-card',
  templateUrl: './activity-detail-card.component.html',
  styleUrls: [ './activity-detail-card.component.css' ]
})
export class ActivityDetailCardComponent implements OnInit {
  @Input() activity: Activity;

  constructor() {}

  ngOnInit(): void {}
}
