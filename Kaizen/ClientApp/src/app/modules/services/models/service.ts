import { ServiceType } from '@modules/services/models/service-type';
import { Product } from '@modules/inventory/products/models/product';
import { Employee } from '@modules/employees/models/employee';
import { Equipment } from '@modules/inventory/equipments/models/equipment';

export interface Service {
	code: string;
	name: string;
	cost: number;
	serviceTypeId: number;
	serviceType?: ServiceType;

	productCodes: string[];
	equipmentCodes: string[];
	employeeCodes: string[];

	products?: Product[];
	equipments?: Equipment[];
	employees?: Employee[];
}
