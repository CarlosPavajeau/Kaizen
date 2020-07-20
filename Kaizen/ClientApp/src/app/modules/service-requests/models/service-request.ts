import { Client } from '@app/modules/clients/models/client';
import { Service } from '@app/modules/services/models/service';
import { ServiceRequestState } from './service-request-state';
import { PeriodicityType } from './periodicity-type';

export interface ServiceRequest {
	code?: number;
	date: Date;
	clientId: string;
	serviceCodes: string[];

	state: ServiceRequestState;
	periodicity: PeriodicityType;

	client?: Client;
	services?: Service[];
}
