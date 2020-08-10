import { ContactPerson } from './contact-person';
import { ClientAddress } from './client-address';
import { Person } from '@shared/models/person';
import { ClientState } from './client-state';

export interface Client extends Person {
  clientType?: string;
  nit?: string;
  busninessName?: string;
  tradeName?: string;
  firstPhoneNumber: string;
  secondPhoneNumber?: string;
  firstLandLine?: string;
  secondLandLine?: string;

  contactPeople: ContactPerson[];
  clientAddress: ClientAddress;

  state: ClientState;
}
