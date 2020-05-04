import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { FormBuilder, AbstractControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { EmployeeExistsValidator } from '@shared/validators/employee-exists-validator';
import { IForm } from '@core/models/form';
import { CharactersValidators } from '@shared/validators/characters-validators';
import { User } from '@core/models/user';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { EMPLOYEE_ROLE } from '@global/roles';

@Component({
	selector: 'app-employee-register',
	templateUrl: './employee-register.component.html',
	styleUrls: [ './employee-register.component.css' ]
})
export class EmployeeRegisterComponent implements OnInit, IForm {
	employeeForm: FormGroup;
	contactForm: FormGroup;
	employeeCharges: EmployeeCharge[];

	public get controls(): { [key: string]: AbstractControl } {
		return this.employeeForm.controls;
	}

	public get contact_controls(): { [key: string]: AbstractControl } {
		return this.contactForm.controls;
	}

	constructor(
		private employeeService: EmployeeService,
		private formBuilder: FormBuilder,
		private authService: AuthenticationService,
		private employeeValidator: EmployeeExistsValidator,
		private notificationsService: NotificationsService
	) {}

	ngOnInit(): void {
		this.loadEmployeeCharges();
		this.initForm();
	}

	loadEmployeeCharges() {
		this.employeeService.getEmployeeCharges().subscribe((employeeCharges) => {
			this.employeeCharges = employeeCharges;
		});
	}

	initForm(): void {
		this.initEmployeeForm();
		this.initContactForm();
	}

	private initEmployeeForm() {
		this.employeeForm = this.formBuilder.group({
			id: [
				'',
				{
					validators: [
						Validators.required,
						Validators.minLength(8),
						Validators.maxLength(10),
						CharactersValidators.numericCharacters
					],
					asyncValidators: [ this.employeeValidator.validate.bind(this.employeeValidator) ],
					updateOn: 'blur'
				}
			],
			firstName: [
				'',
				[
					Validators.required,
					Validators.minLength(2),
					Validators.maxLength(20),
					CharactersValidators.alphabeticCharacters
				]
			],
			secondName: [
				'',
				[ Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters ]
			],
			lastName: [
				'',
				[
					Validators.required,
					Validators.minLength(2),
					Validators.maxLength(20),
					CharactersValidators.alphabeticCharacters
				]
			],
			secondLastname: [
				'',
				[ Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters ]
			],
			employeeCharge: [ '', [ Validators.required ] ]
		});
	}

	private initContactForm() {
		this.contactForm = this.formBuilder.group({
			email: [ '', [ Validators.required, Validators.email ] ],
			phonenumber: [
				'',
				[
					Validators.required,
					Validators.minLength(10),
					Validators.maxLength(10),
					CharactersValidators.numericCharacters
				]
			]
		});
	}

	onSubmit(user: User): void {
		if (user && this.employeeForm.valid) {
			user.email = this.contact_controls['email'].value;
			user.phonenumber = this.contact_controls['phonenumber'].value;
			user.role = EMPLOYEE_ROLE;

			this.authService.registerUser(user).subscribe((userRegister) => {
				const employee: Employee = this.mapEmployee(userRegister.id);

				this.employeeService.saveEmployee(employee).subscribe((employeeRegister) => {
					this.notificationsService.add(`Empleado ${employeeRegister.firstName} registrado con exito`, 'Ok');
					this.onReset();
				});
			});
		}
	}

	mapEmployee(userId: string) {
		const employee: Employee = {
			id: this.controls['id'].value,
			firstName: this.controls['firstName'].value,
			secondName: this.controls['secondName'].value,
			lastName: this.controls['lastName'].value,
			secondLastName: this.controls['secondLastname'].value,
			chargeId: +this.controls['employeeCharge'].value,
			userId: userId
		};
		return employee;
	}

	onReset(): void {
		this.employeeForm.reset();
	}
}
