import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-work-orders',
  templateUrl: './work-orders.component.html'
})
export class WorkOrdersComponent implements OnInit, AfterViewInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  workOrders$: Observable<WorkOrder[]>;
  dataSource: MatTableDataSource<WorkOrder> = new MatTableDataSource<WorkOrder>([]);

  displayedColumns: string[] = [ 'code', 'executionDate', 'arrivalTime', 'departureTime', 'options' ];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private workOrderService: WorkOrderService) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.workOrders$ = this.workOrderService.getWorkOrders();
    this.workOrders$.subscribe((workOrders) => {
      this.dataSource.data = workOrders;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
