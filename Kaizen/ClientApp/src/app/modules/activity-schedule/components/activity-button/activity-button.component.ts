import { Component, OnInit, Input } from '@angular/core';
import { Activity } from '../../models/activity';
import { DatePipe } from '@angular/common';

@Component({
	selector: 'app-activity-button',
	templateUrl: './activity-button.component.html',
	styleUrls: [ './activity-button.component.css' ]
})
export class ActivityButtonComponent implements OnInit {
	@Input() activity: Activity;
	@Input() hourButton: boolean = false;

	constructor() {}

	ngOnInit(): void {}

	buildTooltipMessage(): string {
		let datePipe: DatePipe = new DatePipe('en-US');
		return `Actividad N° ${this.activity.code}, a las ${datePipe.transform(
			this.activity.date,
			'h:mm a'
		)}. Para el cliente ${this.activity.client.lastName} ${this.activity.client
			.firstName}. Click para ver más información.`;
	}
}
