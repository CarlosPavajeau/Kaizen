import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ADMINISTRATOR_ROLE } from '@global/roles';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class AdminGuard implements CanActivate {
	constructor(private authService: AuthenticationService, private router: Router) {}

	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		const role = this.authService.getUserRole();
		if (role !== ADMINISTRATOR_ROLE) {
			this.router.navigateByUrl('/user/profile');
			return false;
		}
		return true;
	}
}
