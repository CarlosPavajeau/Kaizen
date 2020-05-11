import { Component, OnInit, ViewChild, ViewChildren, Directive, Input } from '@angular/core';
import { DashboardCard } from '@app/core/models/dashboard-card';

@Component({
	selector: 'app-dashboard-card',
	templateUrl: './dashboard-card.component.html',
	styleUrls: [ './dashboard-card.component.css' ]
})
export class DashboardCardComponent implements OnInit {
	@Input() dashboardCard: DashboardCard;

	constructor() {}

	ngOnInit(): void {}
}
