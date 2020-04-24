import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticationService } from 'src/app/core/authentication/authentication.service';
import { User } from 'src/app/core/models/user';
import { UserExistsValidator } from 'src/app/shared/validators/user-exists-validator';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerForm: FormGroup;
  @Output() user = new EventEmitter<User>();
  invalidForm: boolean;

  public get controls() {
    return this.registerForm.controls;
  }

  constructor(
    private authService: AuthenticationService,
    private userValidator: UserExistsValidator,
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
    this.registerForm = this.formBuilder.group({
      username: ['', {
        validators: [Validators.required, Validators.minLength(5), Validators.maxLength(15)],
        asyncValidators: [this.userValidator.validate.bind(this.userValidator)],
        updateOn: 'blur'
      }],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const user: User = {
        username: this.controls['username'].value,
        password: this.controls['password'].value
      };
      this.user.emit(user);
    } else {
      this.invalidForm = true;
    }
  }
}
