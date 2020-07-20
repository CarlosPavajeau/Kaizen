import { Client } from '@modules/clients/models/client';
import { Employee } from '@modules/employees/models/employee';
import { PeriodicityType } from '@modules/service-requests/models/periodicity-type';
import { Service } from '@modules/services/models/service';
import { ActivityState } from './activity-state';

export interface Activity {
	code?: number;
	date: Date;
	state: ActivityState;
	clientId: string;
	periodicity: PeriodicityType;

	employeeCodes: string[];
	serviceCodes: string[];

	employees?: Employee[];
	services?: Service[];
	client?: Client;
}
