import { EmployeeCharge } from './employee-charge';
import { Person } from '@shared/models/person';

export interface Employee extends Person {
	chargeId: number;
	employeeCharge?: EmployeeCharge;
}
