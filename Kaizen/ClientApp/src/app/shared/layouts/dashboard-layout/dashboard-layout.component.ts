import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { DASHBOARDS_CARDS } from '@app/global/control-panel-cards';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { DashboardCard } from '@core/models/dashboard-card';
import { EmployeeLocationService } from '@modules/employees/services/employee-location.service';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: [ './dashboard-layout.component.scss' ]
})
export class DashboardLayoutComponent implements OnInit {
  isHandset$: Observable<boolean>;
  userRole: string;
  @ViewChild('drawer', { static: true })
  drawer: MatSidenav;

  isSidenavClose = false;
  isLogout = false;

  menuOptions: DashboardCard[] = [];

  constructor(
    private breakPointObserver: BreakpointObserver,
    private authService: AuthenticationService,
    private employeeLocationService: EmployeeLocationService
  ) {}

  ngOnInit(): void {
    this.isHandset$ = this.breakPointObserver
      .observe(Breakpoints.Handset)
      .pipe(map((result) => result.matches), shareReplay());

    this.userRole = this.authService.getUserRole();
    this.menuOptions = DASHBOARDS_CARDS[this.userRole];
  }

  closeSidenav(): void {
    if (this.drawer.opened) {
      this.drawer.toggle();
      this.isSidenavClose = true;
    }
  }

  toggleSidenav(): void {
    this.drawer.toggle();
    this.isSidenavClose = !this.isSidenavClose;
  }

  logout(): void {
    this.isLogout = true;
    this.authService.logoutUser().subscribe(result => {
      if (result) {
        this.employeeLocationService.endToSendEmployeeLocation();
        location.reload();
      }
    });
  }
}
