import { Client } from '@app/modules/clients/models/client';
import { Service } from '@app/modules/services/models/service';
import { RequestState } from './request-state';
import { PeriodicityType } from './periodicity-type';

export interface ServiceRequest {
	id: number;
	date: Date;
	clientId: string;
	serviceCodes: string[];

	requestState: RequestState;
	periodicity: PeriodicityType;

	client?: Client;
	services?: Service[];
}
