import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/authentication/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isLogged: boolean = false;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.onCheckUser();
  }

  onCheckUser(): void {
    if (this.authService.userLoggedIn()) {
      this.isLogged = true;
    }
  }

  onLogout(): void {
    this.authService.logoutUser();
    window.location.reload();
  }


}
