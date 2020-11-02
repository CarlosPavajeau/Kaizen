import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';

@Component({
  selector: 'app-work-order-detail',
  templateUrl: './work-order-detail.component.html',
  styleUrls: [ './work-order-detail.component.css' ]
})
export class WorkOrderDetailComponent implements OnInit {
  workOrder: WorkOrder;

  constructor(private workOrderService: WorkOrderService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = +this.activatedRoute.snapshot.paramMap.get('code');
    this.workOrderService.getWorkOrder(code).subscribe((workOrder) => {
      this.workOrder = workOrder;
    });
  }
}
