import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { User } from '@core/models/user';
import { UserExistsValidator } from '@shared/validators/user-exists-validator';

@Component({
	selector: 'app-user-register',
	templateUrl: './user-register.component.html',
	styleUrls: [ './user-register.component.css' ]
})
export class UserRegisterComponent implements OnInit {
	registerForm: FormGroup;
	@Output() user = new EventEmitter<User>();
	invalidForm: boolean;

	public get controls() {
		return this.registerForm.controls;
	}

	constructor(private userValidator: UserExistsValidator, private formBuilder: FormBuilder) {}

	ngOnInit(): void {
		this.initForm();
	}

	initForm(): void {
		this.registerForm = this.formBuilder.group({
			username: [
				'',
				{
					validators: [ Validators.required, Validators.minLength(5), Validators.maxLength(15) ],
					asyncValidators: [ this.userValidator.validate.bind(this.userValidator) ],
					updateOn: 'blur'
				}
			],
			password: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
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
