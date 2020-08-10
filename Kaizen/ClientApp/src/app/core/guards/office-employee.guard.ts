import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OFFICE_EMPLOYEE_ROLE } from '@app/global/roles';

@Injectable({
  providedIn: 'root'
})
export class OfficeEmployeeGuard implements CanActivate {
  constructor(private authService: AuthenticationService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const role = this.authService.getUserRole();
    if (role !== OFFICE_EMPLOYEE_ROLE) {
      this.router.navigateByUrl('/user/profile');
      return false;
    }
    return true;
  }
}
