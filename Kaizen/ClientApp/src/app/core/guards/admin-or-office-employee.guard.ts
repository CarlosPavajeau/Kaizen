import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AdminGuard } from './admin.guard';
import { OfficeEmployeeGuard } from './office-employee.guard';

@Injectable({
	providedIn: 'root'
})
export class AdminOrOfficeEmployeeGuard implements CanActivate {
	constructor(private adminGuard: AdminGuard, private officeEmployeeGuard: OfficeEmployeeGuard) {}

	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return this.adminGuard.canActivate(next, state) || this.officeEmployeeGuard.canActivate(next, state);
	}
}
