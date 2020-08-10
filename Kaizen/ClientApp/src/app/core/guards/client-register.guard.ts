import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';
import { OFFICE_EMPLOYEE_ROLE, ADMINISTRATOR_ROLE } from '@app/global/roles';

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
