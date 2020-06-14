import { Client } from '@modules/clients/models/client';
import { Employee } from '@modules/employees/models/employee';
import { PeriodicityType } from '@modules/service-requests/models/periodicity-type';
import { RequestState } from '@modules/service-requests/models/request-state';
import { Service } from '@modules/services/models/service';

export interface Activity {
	code?: number;
	date: Date;
	state: RequestState;
	clientId: string;
	periodicity: PeriodicityType;

	employeeCodes: string[];
	serviceCodes: string[];

	employees?: Employee[];
	services?: Service[];
	client?: Client;
}
