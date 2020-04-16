import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { User } from '../../models/user';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerForm: FormGroup;
  invalidForm: boolean;

  public get controls() {
    return this.registerForm.controls;
  }

  constructor(
    private authService: AuthService,
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
      username: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]],
      email: ['', [Validators.required, Validators.email]],
      phonenumber: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      let user: User = {
        username: this.controls['username'].value,
        email: this.controls['email'].value,
        phonenumber: this.controls['phonenumber'].value,
        password: this.controls['password'].value
      };

      this.authService.registerUser(user)
        .subscribe(user => {
          this.authService.setCurrentUser(user);
          window.location.reload();
        });
    } else {
      this.invalidForm = true;
    }
  }
}
