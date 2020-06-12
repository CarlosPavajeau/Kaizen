import { WorkOrderState } from './work-order-state';
import { Employee } from '@app/modules/employees/models/employee';
import { Activity } from '@app/modules/activity-schedule/models/activity';

export interface WorkOrder {
	code?: number;
	workOrderState: WorkOrderState;
	arrivalTime: Date;
	depatureTime: Date;
	observations: string;
	sector: string;
	executionDate: Date;
	validity: Date;
	activityCode: number;
	employeeId: string;

	employee?: Employee;
	activity?: Activity;
}
