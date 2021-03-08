import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { DASHBOARDS_CARDS } from '@app/global/control-panel-cards';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { DashboardCard } from '@core/models/dashboard-card';
import { EmployeeLocationService } from '@modules/employees/services/employee-location.service';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ADMINISTRATOR_ROLE } from '@global/roles';

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
  isAdmin = false;

  menuOptions: DashboardCard[] = [];

  loading = false;

  constructor(
    private breakPointObserver: BreakpointObserver,
    private authService: AuthenticationService,
    private employeeLocationService: EmployeeLocationService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.isHandset$ = this.breakPointObserver
      .observe(Breakpoints.Handset)
      .pipe(map((result) => result.matches), shareReplay());

    this.userRole = this.authService.getUserRole();
    this.isAdmin = this.userRole == ADMINISTRATOR_ROLE;
    this.menuOptions = DASHBOARDS_CARDS[this.userRole];

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        console.log('loading start')
        this.loading = true;
      } else if (event instanceof NavigationEnd) {
        this.loading = false;
      }
    });
  }

  closeSidenav(): void {
    if (this.drawer.opened) {
      this.drawer.toggle().then(r => {
      });
      this.isSidenavClose = true;
    }
  }

  toggleSidenav(): void {
    this.drawer.toggle().then(r => {
    });
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
