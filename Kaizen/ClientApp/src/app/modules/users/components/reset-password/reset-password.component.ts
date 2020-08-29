import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordModel } from '@modules/users/models/reset-password';
import { UserService } from '@modules/users/services/user.service';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: [ './reset-password.component.css' ]
})
export class ResetPasswordComponent implements OnInit {
  newPassword: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(30)
  ]);
  confirmationPassword: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(30)
  ]);

  token: string;
  email: string;

  updatingPassword = false;
  passwordUpdated = false;

  hideNewPassword = true;
  hideConfirmationPassword = true;

  passwordNotMatch = false;

  constructor(
    private userService: UserService,
    private actiatedRoute: ActivatedRoute,
    private router: Router,
    private notificationsService: NotificationsService,
    private errorHandler: HttpErrorHandlerService
  ) {}

  ngOnInit(): void {
    this.actiatedRoute.queryParamMap.subscribe((queryParams) => {
      this.token = queryParams.get('token');
      this.email = queryParams.get('email');
    });
  }

  onSubmit(): void {
    if (this.newPassword.valid && this.confirmationPassword.valid) {
      this.newPassword.disable();
      this.confirmationPassword.disable();
      this.updatingPassword = true;

      if (this.newPassword.value === this.confirmationPassword.value) {
        const resetPasswordModel: ResetPasswordModel = {
          token: this.token,
          newPassword: this.newPassword.value
        };
        this.userService
          .resetPassword(this.email, resetPasswordModel)
          .pipe(
            catchError((error: any) => {
              this.errorHandler.handleHttpError(error, () => {
                this.onReset();
              });
              return of(null);
            })
          )
          .subscribe((result) => {
            if (result) {
              this.notificationsService.showSuccessMessage('Contraseña de acceso modificado con éxito.', () => {
                this.router.navigateByUrl('/user/login');
              });
            }
          });
      } else {
        this.onReset();
        this.passwordNotMatch = true;
        setTimeout(() => {
          this.passwordNotMatch = false;
        }, 3000);
      }
    }
  }

  onReset(): void {
    this.updatingPassword = false;
    this.newPassword.enable();
    this.confirmationPassword.enable();
  }
}
