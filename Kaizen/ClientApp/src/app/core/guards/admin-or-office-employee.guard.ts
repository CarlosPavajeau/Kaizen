import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ADMINISTRATOR_ROLE, OFFICE_EMPLOYEE_ROLE } from '@global/roles';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminOrOfficeEmployeeGuard implements CanActivate {
  constructor(private authService: AuthenticationService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const role = this.authService.getUserRole();
    if (role === OFFICE_EMPLOYEE_ROLE || role === ADMINISTRATOR_ROLE) {
      return true;
    }

    this.router.navigateByUrl('user/profile');
    return false;
  }
}
