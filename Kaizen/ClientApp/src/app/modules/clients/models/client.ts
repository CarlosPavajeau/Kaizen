import { ContactPerson } from './contact-person';
import { ClientAddress } from './client-address';
import { Person } from '@shared/models/person';

export interface Client extends Person {
	clientType?: string;
	NIT?: string;
	busninessName?: string;
	tradeName?: string;
	firstPhoneNumber: string;
	secondPhoneNumber?: string;
	firstLandline?: string;
	secondLandline?: string;

	contactPeople: ContactPerson[];
	clientAddress: ClientAddress;
}
