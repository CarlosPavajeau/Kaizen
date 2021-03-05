import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ServiceTypeRegisterComponent } from '@modules/services/components/service-type-register/service-type-register.component';
import { ServiceType } from '@modules/services/models/service-type';
import { ServiceService } from '@modules/services/services/service.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-types',
  templateUrl: './service-types.component.html',
  styleUrls: [ './service-types.component.scss' ]
})
export class ServiceTypesComponent implements OnInit, AfterViewInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceTypes: ServiceType[] = [];
  serviceTypes$: Observable<ServiceType[]>;

  dataSource: MatTableDataSource<ServiceType> = new MatTableDataSource<ServiceType>(this.serviceTypes);
  displayedColumns: string[] = [ 'code', 'name', 'options' ];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private serviceService: ServiceService, private matDialog: MatDialog,) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.serviceTypes$ = this.serviceService.getServiceTypes();

    this.serviceTypes$.subscribe((serviceTypes: ServiceType[]) => {
      this.serviceTypes = serviceTypes;
      this.dataSource.data = this.serviceTypes;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  registerServiceType(): void {
    const matDialogRef = this.matDialog.open(ServiceTypeRegisterComponent, {
      width: '700px'
    });

    matDialogRef.afterClosed().subscribe((serviceType: ServiceType) => {
      if (serviceType) {
        this.serviceService.saveServiceType(serviceType).subscribe((savedServiceType: ServiceType) => {
          if (savedServiceType) {
            this.serviceTypes.push(savedServiceType);
            this.dataSource.data = this.serviceTypes;
          }
        });
      }
    });
  }
}
