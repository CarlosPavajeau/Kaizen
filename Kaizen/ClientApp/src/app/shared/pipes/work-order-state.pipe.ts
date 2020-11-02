import { Pipe, PipeTransform } from '@angular/core';
import { WorkOrderState } from '@modules/work-orders/models/work-order-state';

@Pipe({
  name: 'workOrderState'
})
export class WorkOrderStatePipe implements PipeTransform {
  transform(value: WorkOrderState): string {
    switch (value) {
      case WorkOrderState.Generated:
        return 'Generada';
      case WorkOrderState.Confirmed:
        return 'Confirmada';
      case WorkOrderState.Canceled:
        return 'Cancelada';
      case WorkOrderState.Valid:
        return 'Válida';
      case WorkOrderState.Expired:
        return 'Expirada';
    }
  }
}
