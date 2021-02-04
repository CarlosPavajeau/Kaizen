import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: [ './nav-menu.component.scss' ]
})
export class NavMenuComponent implements OnInit {
  isLogged = false;
  isLogout = false;

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    this.onCheckUser();
  }

  onCheckUser(): void {
    if (this.authService.userLoggedIn()) {
      this.isLogged = true;
    }
  }

  onLogout(): void {
    this.isLogout = true;
    this.authService.logoutUser().subscribe(result => {
      if (result) {
        location.reload();
      }
    });
  }
}
