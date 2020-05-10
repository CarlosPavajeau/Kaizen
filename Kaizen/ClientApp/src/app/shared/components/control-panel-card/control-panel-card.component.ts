import { Component, OnInit, ViewChild, ViewChildren, Directive, Input } from '@angular/core';
import { ControlPanelCard } from '@core/models/control-panel-card';

@Component({
	selector: 'app-control-panel-card',
	templateUrl: './control-panel-card.component.html',
	styleUrls: [ './control-panel-card.component.css' ]
})
export class ControlPanelCardComponent implements OnInit {
	@Input() controlCard: ControlPanelCard;

	constructor() {}

	ngOnInit(): void {
		console.log(this.controlCard);
	}
}
