import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Service } from '@modules/services/models/service';
import { ServiceService } from '@modules/services/services/service.service';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: [ './services.component.css' ]
})
export class ServicesComponent implements OnInit, AfterViewInit {
  services: Service[] = [];
  dataSource: MatTableDataSource<Service> = new MatTableDataSource<Service>(this.services);
  displayedColumns: string[] = [ 'code', 'name', 'type', 'cost', 'options' ];
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private serviceService: ServiceService) {}

  ngOnInit(): void {
    this.loadServices();
  }

  private loadServices(): void {
    this.serviceService.getServices().subscribe((services) => {
      this.services = services;
      this.dataSource.data = this.services;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
