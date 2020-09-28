import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/core/authentication/authentication.service';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: [ './dashboard-layout.component.css' ]
})
export class DashboardLayoutComponent implements OnInit, AfterViewInit {
  isHandset$: Observable<boolean>;
  userType: string;
  @ViewChild('drawer', { static: true })
  drawer: MatSidenav;

  isSidenavClose = false;

  constructor(
    private breakPointObserver: BreakpointObserver,
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    console.log(this.drawer);
  }

  ngOnInit(): void {
    this.isHandset$ = this.breakPointObserver
      .observe(Breakpoints.Handset)
      .pipe(map((result) => result.matches), shareReplay());

    this.userType = this.authService.getUserRole();
  }

  closeSidenav(): void {
    this.drawer.close();
    this.isSidenavClose = true;
  }

  toggleSidenav(): void {
    this.drawer.toggle();
    this.isSidenavClose = !this.isSidenavClose;
  }

  logout(): void {
    this.authService.logoutUser();
    location.reload();
  }
}
