import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { User } from '@core/models/user';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsService } from '@shared/services/notifications.service';
import { alphabeticCharacters, numericCharacters } from '@shared/validators/characters-validators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-register',
  templateUrl: './employee-register.component.html',
  styleUrls: [ './employee-register.component.scss' ]
})
export class EmployeeRegisterComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  employeeForm: FormGroup;
  contactForm: FormGroup;
  contractForm: FormGroup;

  employeeCharges$: Observable<EmployeeCharge[]>;
  savingData = false;

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
    private notificationsService: NotificationsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEmployeeCharges();
    this.initForm();
  }

  loadEmployeeCharges() {
    this.employeeCharges$ = this.employeeService.getEmployeeCharges();
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
          validators: [ Validators.required, Validators.minLength(8), Validators.maxLength(10), numericCharacters() ],
          updateOn: 'blur'
        }
      ],
      firstName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondName: [ '', [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ],
      lastName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondLastname: [ '', [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ],
      employeeCharge: [ '', [ Validators.required ] ]
    });
  }

  private initContactForm() {
    this.contactForm = this.formBuilder.group({
      email: [ '', [ Validators.required, Validators.email ] ],
      phonenumber: [
        '',
        [ Validators.required, Validators.minLength(10), Validators.maxLength(10), numericCharacters() ]
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
      this.savingData = true;

      user.email = this.contact_controls['email'].value;
      user.phonenumber = this.contact_controls['phonenumber'].value;

      const employee: Employee = this.mapEmployee(user);

      this.employeeService.saveEmployee(employee).subscribe((employeeRegister) => {
        this.notificationsService.showSuccessMessage(
          `El empleado ${employeeRegister.firstName} ${employeeRegister.lastName} fue registrado con éxito`,
          () => {
            this.router.navigateByUrl('/employees');
          }
        );
      });
    }
  }

  mapEmployee(user: User): Employee {
    return {
      ...this.employeeForm.value,
      chargeId: +this.controls['employeeCharge'].value,
      contractCode: this.contract_controls['contractCode'].value,
      employeeContract: { ...this.contractForm.value },
      user: user
    };
  }
}
