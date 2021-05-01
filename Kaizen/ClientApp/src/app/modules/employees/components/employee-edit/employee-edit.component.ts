import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from '@app/shared/services/notifications.service';
import { IForm } from '@core/models/form';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { EmployeeContract } from '@modules/employees/models/employee-contract';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html'
})
export class EmployeeEditComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  employeeContractForm: FormGroup;

  employee$: Observable<Employee>;
  employeeCharges$: Observable<EmployeeCharge[]>;

  updatingEmployee = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.employeeContractForm.controls;
  }

  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private dialogsService: DialogsService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.employee$ = this.employeeService.getEmployee(id);
    this.employeeCharges$ = this.employeeService.getEmployeeCharges();

    this.employee$.subscribe((employee) => {
      this.afterLoadEmployee(employee);
    });
  }

  private afterLoadEmployee(employee: Employee): void {
    this.employeeContractForm.setValue({
      ...employee.employeeContract,
      employeeCharge: employee.employeeCharge.id
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

  updateChargeAndContract(employee: Employee): void {
    if (this.employeeContractForm.valid) {
      const employeeContract: EmployeeContract = {
        ...this.employeeContractForm.value
      };

      employee.chargeId = +this.controls['employeeCharge'].value;
      employee.employeeContract = employeeContract;
      employee.contractCode = employeeContract.contractCode;

      this.updatingEmployee = true;
      this.employeeService.updateEmployee(employee).subscribe((updatedEmployee) => {
        if (updatedEmployee) {
          this.dialogsService.showSuccessDialog(
            `Los datos del empleado ${ updatedEmployee.firstName } ${ updatedEmployee.lastName } fueron actualizados con éxito.`,
            () => {
              this.updatingEmployee = false;
              this.router.navigateByUrl('/employees');
            }
          );
        }
      });
    }
  }
}
