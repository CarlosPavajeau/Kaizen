import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-work-order-detail',
  templateUrl: './work-order-detail.component.html',
  styleUrls: [ './work-order-detail.component.scss' ]
})
export class WorkOrderDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  workOrder$: Observable<WorkOrder>;

  constructor(private workOrderService: WorkOrderService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = +this.activatedRoute.snapshot.paramMap.get('code');
    this.workOrder$ = this.workOrderService.getWorkOrder(code);
  }
}
