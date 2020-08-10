import { EmployeeCharge } from './employee-charge';
import { Person } from '@shared/models/person';
import { EmployeeContract } from './employee-contract';

export interface Employee extends Person {
  chargeId: number;
  contractCode: string;
  employeeCharge?: EmployeeCharge;
  employeeContract?: EmployeeContract;
}
