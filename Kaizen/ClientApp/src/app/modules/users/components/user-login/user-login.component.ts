import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { IForm } from '@core/models/form';
import { LoginRequest } from '@core/models/login-request';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: [ './user-login.component.css' ]
})
export class UserLoginComponent implements OnInit, IForm {
  loginForm: FormGroup;
  loading = false;
  hide = true;

  public get controls() {
    return this.loginForm.controls;
  }

  constructor(
    private authService: AuthenticationService,
    private formBuilder: FormBuilder,
    private errorHandler: HttpErrorHandlerService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.loginForm = this.formBuilder.group({
      usernameOrEmail: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(15) ] ],
      password: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.loading = true;
      const loginRequest: LoginRequest = {
        usernameOrEmail: this.controls['usernameOrEmail'].value,
        password: this.controls['password'].value
      };

      this.authService
        .loginUser(loginRequest)
        .pipe(
          catchError((error: any) => {
            this.errorHandler.handleHttpError(error, () => {
              this.loading = false;
            });
            return of(null);
          })
        )
        .subscribe((user) => {
          if (user) {
            this.authService.setCurrentUser(user);
            window.location.reload();
          }
        });
    }
  }
}
