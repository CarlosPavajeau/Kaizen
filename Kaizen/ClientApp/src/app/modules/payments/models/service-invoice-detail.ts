import { Service } from '@modules/services/models/service';

export interface ServiceInvoiceDetail {
	id: number;
	serviceCode: string;
	serviceName: string;

	detail?: Service;
	total: number;
}
