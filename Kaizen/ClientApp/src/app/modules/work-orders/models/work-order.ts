import { WorkOrderState } from './work-order-state';
import { Employee } from '@app/modules/employees/models/employee';
import { Activity } from '@app/modules/activity-schedule/models/activity';
import { Sector } from './sector';

export interface WorkOrder {
  code?: number;
  workOrderState: WorkOrderState;
  arrivalTime: Date;
  depatureTime?: Date;
  observations?: string;
  sectorId: number;
  executionDate: Date;
  validity?: Date;
  clientSignature?: string;
  activityCode: number;
  employeeId: string;

  employee?: Employee;
  activity?: Activity;
  sector?: Sector;
}
