import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from '@app/shared/services/notifications.service';
import { IForm } from '@core/models/form';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { EmployeeContract } from '@modules/employees/models/employee-contract';
import { EmployeeService } from '@modules/employees/services/employee.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: [ './employee-edit.component.css' ]
})
export class EmployeeEditComponent implements OnInit, IForm {
  employee: Employee;
  employeeContractForm: FormGroup;
  employeeCharges: EmployeeCharge[];

  updatingEmployee = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.employeeContractForm.controls;
  }

  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.employeeService.getEmployee(id).subscribe((employee) => {
      this.employee = employee;
      console.log(this.employee);
      this.afterLoadEmployee();
    });

    this.employeeService.getEmployeeCharges().subscribe((employeeCharges) => {
      this.employeeCharges = employeeCharges;
    });
  }

  private afterLoadEmployee(): void {
    this.employeeContractForm.setValue({
      ...this.employee.employeeContract,
      employeeCharge: this.employee.employeeCharge.id
    });
  }

  initForm(): void {
    this.employeeContractForm = this.formBuilder.group({
      contractCode: [ '', [ Validators.required, Validators.maxLength(30), Validators.minLength(3) ] ],
      startDate: [ '', [ Validators.required ] ],
      endDate: [ '', [ Validators.required ] ],
      employeeCharge: [ '', [ Validators.required ] ]
    });
  }

  updateChargeAndContract(): void {
    if (this.employeeContractForm.valid) {
      const employeeContract: EmployeeContract = {
        ...this.employeeContractForm.value
      };

      this.employee.chargeId = +this.controls['employeeCharge'].value;
      this.employee.employeeContract = employeeContract;
      this.employee.contractCode = employeeContract.contractCode;

      this.updatingEmployee = true;
      this.employeeService.updateEmployee(this.employee).subscribe((updatedEmployee) => {
        if (updatedEmployee) {
          this.notificationsService.showSuccessMessage(
            `Los datos del empleado ${updatedEmployee.firstName} ${updatedEmployee.lastName} fueron actualizados con éxito.`,
            () => {
              this.employee = updatedEmployee;
              this.updatingEmployee = false;
            }
          );
        }
      });
    }
  }
}
