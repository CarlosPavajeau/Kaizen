import { Person } from '@shared/models/person';
import { ClientAddress } from './client-address';
import { ClientState } from './client-state';
import { ContactPerson } from './contact-person';

export interface Client extends Person {
  clientType?: string;
  nit?: string;
  businessName?: string;
  tradeName?: string;
  firstPhoneNumber: string;
  secondPhoneNumber?: string;
  firstLandLine?: string;
  secondLandLine?: string;

  contactPeople: ContactPerson[];
  clientAddress: ClientAddress;

  state: ClientState;
}
