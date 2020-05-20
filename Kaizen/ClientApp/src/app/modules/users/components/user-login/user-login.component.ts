import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { LoginRequest } from '@core/models/login-request';
import { IForm } from '@core/models/form';

@Component({
	selector: 'app-user-login',
	templateUrl: './user-login.component.html',
	styleUrls: [ './user-login.component.css' ]
})
export class UserLoginComponent implements OnInit, IForm {
	loginForm: FormGroup;
	invalidUserOrEmail: boolean = false;
	invalidPassword: boolean = false;
	invalidForm: boolean = false;
	loading: boolean = false;
	hide: boolean = true;

	public get controls() {
		return this.loginForm.controls;
	}

	constructor(private authService: AuthenticationService, private formBuilder: FormBuilder) {}

	ngOnInit(): void {
		this.initForm();
	}

	initForm(): void {
		this.loginForm = this.formBuilder.group({
			usernameOrEmail: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(30) ] ],
			password: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
		});
	}

	onSubmit(): void {
		if (this.loginForm.valid) {
			this.loading = true;
			let loginRequest: LoginRequest = {
				usernameOrEmail: this.controls['usernameOrEmail'].value,
				password: this.controls['password'].value
			};

			this.authService.loginUser(loginRequest).subscribe(
				(user) => {
					if (user) {
						this.authService.setCurrentUser(user);
						window.location.reload();
					} else {
						this.invalidUserOrEmail = true;
					}
				},
				(error) => {
					this.loading = false;
					throw error;
				}
			);
		} else {
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
