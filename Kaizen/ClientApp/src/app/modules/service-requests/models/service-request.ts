import { Client } from '@modules/clients/models/client';
import { Service } from '@modules/services/models/service';
import { PeriodicityType } from './periodicity-type';
import { ServiceRequestState } from './service-request-state';

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
