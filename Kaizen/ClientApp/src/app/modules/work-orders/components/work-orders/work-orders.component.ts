import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { WorkOrder } from '../../models/work-order';
import { WorkOrderService } from '../../service/work-order.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-work-orders',
  templateUrl: './work-orders.component.html',
  styleUrls: [ './work-orders.component.css' ]
})
export class WorkOrdersComponent implements OnInit, AfterViewInit {
  workOrders: WorkOrder[] = [];
  dataSource: MatTableDataSource<WorkOrder> = new MatTableDataSource<WorkOrder>(this.workOrders);
  displayedColumns: string[] = [ 'code', 'executionDate', 'arrivalTime', 'depatureTime', 'options' ];
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private workOrderService: WorkOrderService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.workOrderService.getWorkOrders().subscribe((workOrders) => {
      this.workOrders = workOrders;
      this.dataSource.data = workOrders;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
