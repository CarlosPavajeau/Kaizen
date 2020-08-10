import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ActivatedRoute } from '@angular/router';
import { Employee } from '@modules/employees/models/employee';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: [ './employee-detail.component.css' ]
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;

  constructor(private employeeService: EmployeeService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    this.employeeService.getEmployee(id).subscribe((employee) => {
      this.employee = employee;
    });
  }
}
