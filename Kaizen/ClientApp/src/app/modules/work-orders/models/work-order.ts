import { Activity } from '@modules/activity-schedule/models/activity';
import { Employee } from '@modules/employees/models/employee';
import { Sector } from './sector';
import { WorkOrderState } from './work-order-state';

export interface WorkOrder {
  code?: number;
  workOrderState: WorkOrderState;
  arrivalTime: Date;
  departureTime?: Date;
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
