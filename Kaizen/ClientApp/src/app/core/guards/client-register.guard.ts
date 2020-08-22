import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { ADMINISTRATOR_ROLE, OFFICE_EMPLOYEE_ROLE } from '@global/roles';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientRegisterGuard implements CanActivate {
  constructor(private authService: AuthenticationService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.authService.userLoggedIn()) {
      return true;
    }

    const role = this.authService.getUserRole();
    if ([ ADMINISTRATOR_ROLE, OFFICE_EMPLOYEE_ROLE ].includes(role)) {
      return true;
    }

    this.router.navigateByUrl('/user/profile');
    return false;
  }
}
