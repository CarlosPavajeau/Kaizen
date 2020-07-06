import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';

@Component({
	selector: 'app-nav-menu',
	templateUrl: './nav-menu.component.html',
	styleUrls: [ './nav-menu.component.css' ]
})
export class NavMenuComponent implements OnInit {
	isLogged: boolean = false;

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
		this.authService.logoutUser();
		localStorage.removeItem('current_person');
		window.location.reload();
	}
}
