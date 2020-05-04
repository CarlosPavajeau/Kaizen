import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { EMPLOYEE_ROLE } from '@global/roles';

@Injectable({
	providedIn: 'root'
})
export class EmployeeGuard implements CanActivate {
	constructor(private authService: AuthenticationService, private router: Router) {}

	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		var role = this.authService.getUserRole();
		if (role !== EMPLOYEE_ROLE) {
			this.router.navigateByUrl('/user/profile');
			return false;
		}
		return true;
	}
}
