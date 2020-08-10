import { Component, OnInit } from '@angular/core';
import { DashboardCard } from '@app/core/models/dashboard-card';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { DASHBOARS_CARDS } from '@app/global/control-panel-cards';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: [ './dashboard.component.css' ]
})
export class DashboardComponent implements OnInit {
  controlCards: DashboardCard[] = [];

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    const userRole = this.authService.getUserRole();
    this.controlCards = DASHBOARS_CARDS[userRole];
  }
}
