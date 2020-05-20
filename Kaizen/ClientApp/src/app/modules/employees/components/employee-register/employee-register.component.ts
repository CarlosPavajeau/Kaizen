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
import { OFFICE_EMPLOYEE_ROLE, TECHNICAL_EMPLOYEE_ROLE, ADMINISTRATOR_ROLE, EMPLOYEE_ROLE } from '@global/roles';
import { Router } from '@angular/router';

@Component({
	selector: 'app-employee-register',
	templateUrl: './employee-register.component.html',
	styleUrls: [ './employee-register.component.css' ]
})
export class EmployeeRegisterComponent implements OnInit, IForm {
	employeeForm: FormGroup;
	contactForm: FormGroup;
	contractForm: FormGroup;
	employeeCharges: EmployeeCharge[];

	public get controls(): { [key: string]: AbstractControl } {
		return this.employeeForm.controls;
	}

	public get contact_controls(): { [key: string]: AbstractControl } {
		return this.contactForm.controls;
	}

	public get contract_controls(): { [key: string]: AbstractControl } {
		return this.contractForm.controls;
	}

	constructor(
		private employeeService: EmployeeService,
		private formBuilder: FormBuilder,
		private authService: AuthenticationService,
		private employeeValidator: EmployeeExistsValidator,
		private notificationsService: NotificationsService,
		private router: Router
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
		this.initContractForm();
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

	private initContractForm() {
		this.contractForm = this.formBuilder.group({
			contractCode: [ '', [ Validators.required, Validators.maxLength(30), Validators.minLength(3) ] ],
			startDate: [ '', [ Validators.required ] ],
			endDate: [ '', [ Validators.required ] ]
		});
	}

	onSubmit(user: User): void {
		if (user && this.employeeForm.valid && this.contactForm.valid && this.contractForm.valid) {
			user.email = this.contact_controls['email'].value;
			user.phonenumber = this.contact_controls['phonenumber'].value;

			const employeeChargeId = +this.controls['employeeCharge'].value;
			let role: string;
			if (employeeChargeId == 1 || employeeChargeId == 2) {
				role = ADMINISTRATOR_ROLE;
			} else if (employeeChargeId == 5) {
				role = OFFICE_EMPLOYEE_ROLE;
			} else if (employeeChargeId == 7) {
				role = TECHNICAL_EMPLOYEE_ROLE;
			} else {
				role = EMPLOYEE_ROLE;
			}
			user.role = role;

			this.authService.registerUser(user).subscribe((userRegister) => {
				const employee: Employee = this.mapEmployee(userRegister.id);

				this.employeeService.saveEmployee(employee).subscribe((employeeRegister) => {
					this.notificationsService.add(`Empleado ${employeeRegister.firstName} registrado con exito`, 'Ok');
					this.onReset();
					this.router.navigateByUrl('/employees');
				});
			});
		}
	}

	mapEmployee(userId: string): Employee {
		return {
			id: this.controls['id'].value,
			firstName: this.controls['firstName'].value,
			secondName: this.controls['secondName'].value,
			lastName: this.controls['lastName'].value,
			secondLastName: this.controls['secondLastname'].value,
			chargeId: +this.controls['employeeCharge'].value,
			contractCode: this.contract_controls['contractCode'].value,
			employeeContract: {
				contractCode: this.contract_controls['contractCode'].value,
				startDate: this.contract_controls['startDate'].value,
				endDate: this.contract_controls['endDate'].value
			},
			userId: userId
		};
	}

	onReset(): void {
		this.employeeForm.reset();
	}
}
