import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeeChargeRegisterComponent } from '@modules/employees/components/employee-charge-register/employee-charge-register.component';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-charges',
  templateUrl: './employee-charges.component.html',
  styleUrls: [ './employee-charges.component.scss' ]
})
export class EmployeeChargesComponent implements OnInit, AfterViewInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  employeeCharges$: Observable<EmployeeCharge[]>;
  employeeCharges: EmployeeCharge[] = [];

  dataSource: MatTableDataSource<EmployeeCharge> = new MatTableDataSource<EmployeeCharge>(this.employeeCharges);
  displayedColumns: string[] = [ 'id', 'charge', 'options' ];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private employeeService: EmployeeService, private matDialog: MatDialog) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.employeeCharges$ = this.employeeService.getEmployeeCharges();
    this.employeeCharges$.subscribe((employeeCharges: EmployeeCharge[]) => {
      this.employeeCharges = employeeCharges;
      this.dataSource.data = this.employeeCharges;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  registerEmployeeCharge(): void {
    const matDialogRef = this.matDialog.open(EmployeeChargeRegisterComponent, {
      width: '700px'
    });

    matDialogRef.afterClosed().subscribe((employeeCharge: EmployeeCharge) => {
      if (employeeCharge) {
        this.employeeService.saveEmployeeCharge(employeeCharge).subscribe((savedEmployeeCharge: EmployeeCharge) => {
          if (savedEmployeeCharge) {
            this.employeeCharges.push(savedEmployeeCharge);
            this.dataSource.data = this.employeeCharges;
          }
        });
      }
    });
  }
}
