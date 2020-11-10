import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: [ './employees.component.css' ]
})
export class EmployeesComponent implements OnInit, AfterViewInit {
  employees: Employee[] = [];
  dataSource: MatTableDataSource<Employee> = new MatTableDataSource<Employee>(this.employees);
  displayedColumns: string[] = [ 'id', 'name', 'employeeCharge', 'options' ];
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  private loadEmployees() {
    this.employeeService.getEmployees().subscribe((employees) => {
      this.employees = employees;
      this.dataSource.data = this.employees;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }
}
