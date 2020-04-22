import { ContactPerson } from "./contact-person";
import { ClientAddress } from "./client-address";

export interface Client {
  id: string;
  firstName: string;
  secondName?: string;
  lastName: string;
  secondLastName?: string;
  clientType?: string;
  busninessName?: string;
  tradeName?: string;
  firstPhoneNumber: string;
  secondPhoneNumber?: string;
  firstLandline: string;
  secondLandline?: string;

  contactPeople: ContactPerson[];
  clientAddress: ClientAddress;
}
