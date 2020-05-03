import { ServiceType } from '@modules/services/models/service-type';

export interface Service {
	code: string;
	name: string;
	cost: number;
	serviceTypeId: number;
	serviceType?: ServiceType;
}
