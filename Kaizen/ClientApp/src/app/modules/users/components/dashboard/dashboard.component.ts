import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { DashboardCard } from '@core/models/dashboard-card';
import { DASHBOARDS_CARDS } from '@global/control-panel-cards';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: [ './dashboard.component.scss' ]
})
export class DashboardComponent implements OnInit {
  controlCards: DashboardCard[] = [];

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    const userRole = this.authService.getUserRole();
    this.controlCards = DASHBOARDS_CARDS[userRole];
  }
}
