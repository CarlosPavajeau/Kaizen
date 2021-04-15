import { WorkOrder } from '@modules/work-orders/models/work-order';

export interface Certificate {
  id: number;
  validity: Date;

  workOrder?: WorkOrder;
}
