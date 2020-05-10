import { Component, OnInit } from '@angular/core';
import { ControlPanelCard } from '@app/core/models/control-panel-card';

@Component({
	selector: 'app-control-panel',
	templateUrl: './control-panel.component.html',
	styleUrls: [ './control-panel.component.css' ]
})
export class ControlPanelComponent implements OnInit {
	controlCards: ControlPanelCard[] = [
		{
			title: 'Datos de acceso',
			imgUrl: 'assets/images/ecolplag_bird.png',
			url: '/access'
		},
		{
			title: 'Facturas',
			imgUrl: 'assets/images/ecolplag_bird.png',
			url: '/contact'
		}
	];
	constructor() {}

	ngOnInit(): void {}
}
