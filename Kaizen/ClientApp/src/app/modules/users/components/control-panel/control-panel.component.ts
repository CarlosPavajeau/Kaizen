import { Component, OnInit } from '@angular/core';
import { ControlPanelCard } from '@core/models/control-panel-card';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { CONTROL_PANEL_CARDS } from '@app/global/control-panel-cards';

@Component({
	selector: 'app-control-panel',
	templateUrl: './control-panel.component.html',
	styleUrls: [ './control-panel.component.css' ]
})
export class ControlPanelComponent implements OnInit {
	controlCards: ControlPanelCard[] = [];

	constructor(private authService: AuthenticationService) {}

	ngOnInit(): void {
		const userRole = this.authService.getUserRole();
		this.controlCards = CONTROL_PANEL_CARDS[userRole];
	}
}
