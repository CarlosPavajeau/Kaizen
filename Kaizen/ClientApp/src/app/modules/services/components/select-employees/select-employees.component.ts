import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { IForm } from '@core/models/form';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-select-employees',
  templateUrl: './select-employees.component.html',
  styleUrls: [ './select-employees.component.scss' ]
})
export class SelectEmployeesComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  employees$: Observable<Employee[]>;

  selectedEmployees: Employee[] = [];

  selectEmployeesForm: FormGroup;
  @ViewChildren('employeesListSelection') employeesListSelection: QueryList<MatSelectionList>;

  get controls(): { [key: string]: AbstractControl } {
    return this.selectEmployeesForm.controls;
  }

  constructor(private employeeService: EmployeeService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    this.employees$ = this.employeeService.getTechnicians();
    this.employees$.pipe(delay(250)).subscribe((employees) => {
      this.selectEmployees();
    });
  }

  initForm(): void {
    this.selectEmployeesForm = this.formBuilder.group({
      employeeId: [ '' ],
      showSelectedEmployees: [ false ],
      employeeCodes: [ '', [ Validators.required ] ]
    });

    this.controls['employeeId'].valueChanges.pipe(delay(100)).subscribe((value) => {
      this.selectEmployees();
    });

    this.controls['showSelectedEmployees'].valueChanges.subscribe((value) => {
      if (value) {
        this.controls['employeeId'].disable();
      } else {
        this.controls['employeeId'].enable();
      }
    });
  }

  selectEmployees(): void {
    if (
      this.selectedEmployees.length === 0 ||
      this.employeesListSelection.first === undefined ||
      this.employeesListSelection.first.options === undefined
    ) {
      return;
    }

    const selectedOptions = this.employeesListSelection.first.options.filter((option) => {
      return this.selectedEmployees.some((e) => e.id === option.value.id);
    });

    this.employeesListSelection.first.selectedOptions.select(...selectedOptions);
  }

  onSelectEmployee(event: MatSelectionListChange): void {
    const option = event.option;
    const value = option.value;
    if (option.selected) {
      this.selectedEmployees.push(value);
    } else {
      const index = this.selectedEmployees.indexOf(value);
      if (index !== -1) {
        this.selectedEmployees.splice(index, 1);
      }
    }
  }

  get valid(): boolean {
    return this.selectEmployeesForm.valid;
  }

  get invalid(): boolean {
    return !this.valid;
  }

  get value(): any {
    if (this.selectedEmployees.length === 0) {
      return null;
    }
    return this.controls['employeeCodes'].value.map((p: Employee) => p.id);
  }

  setValue(value: Employee[]): void {
    this.selectedEmployees = value;
    this.selectEmployees();
  }
}
