import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';

@Component({
  selector: 'app-activity-button',
  templateUrl: './activity-button.component.html',
  styleUrls: [ './activity-button.component.css' ]
})
export class ActivityButtonComponent implements OnInit {
  @Input() activity: Activity;
  @Input() hourButton = false;

  constructor() {}

  ngOnInit(): void {}

  buildTooltipMessage(): string {
    const datePipe: DatePipe = new DatePipe('en-US');
    return `Actividad N° ${this.activity.code}, a las ${datePipe.transform(
      this.activity.date,
      'h:mm a'
    )}. Para el cliente ${this.activity.client.lastName} ${this.activity.client
      .firstName}. Click para ver más información.`;
  }
}
