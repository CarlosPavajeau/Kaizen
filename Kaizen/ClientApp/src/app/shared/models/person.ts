import { User } from '@app/core/models/user';

export interface Person {
  id: string;
  firstName: string;
  secondName?: string;
  lastName: string;
  secondLastName?: string;

  user?: User;
}
