import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-forgotten-password',
  templateUrl: './forgotten-password.component.html',
  styleUrls: [ './forgotten-password.component.css' ]
})
export class ForgottenPasswordComponent implements OnInit {
  usernameOrEmail: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(5),
    Validators.maxLength(15)
  ]);
  sendingRequest = false;
  requestResult = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.usernameOrEmail.valid) {
      this.sendingRequest = true;
      this.usernameOrEmail.disable();

      const usernameOrEmail = this.usernameOrEmail.value;
      this.userService.forgottenPassword(usernameOrEmail).subscribe((result) => {
        this.requestResult = result;
      });
    }
  }
}
