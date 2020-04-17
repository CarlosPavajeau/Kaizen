import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/authentication/authentication.service';
import { LoginRequest } from 'src/app/core/models/login-request';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidUserOrEmail: boolean = false;
  invalidPassword: boolean = false;
  invalidForm: boolean = false;

  public get controls() {
    return this.loginForm.controls;
  }

  constructor(
    private authService: AuthenticationService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.authService.userLoggedIn()) {
      this.router.navigateByUrl('/user/profile');
    } else {
      this.initForm();
    }
  }

  initForm(): void {
    this.loginForm = this.formBuilder.group({
      usernameOrEmail: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
    })
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      let user: LoginRequest = {
        usernameOrEmail: this.controls['usernameOrEmail'].value,
        password: this.controls['password'].value
      };

      this.authService.loginUser(user)
        .subscribe(user => {
          if (user) {
            this.authService.setCurrentUser(user);
            window.location.reload();
          } else {
            this.invalidUserOrEmail = true;
          }
        });
    }
    else {
      this.invalidForm = true;
    }

    this.onReset();
  }

  onReset(): void {
    setTimeout(() => {
      this.invalidForm = false;
      this.invalidPassword = false;
      this.invalidUserOrEmail = false;
    }, 4000);
  }

}
