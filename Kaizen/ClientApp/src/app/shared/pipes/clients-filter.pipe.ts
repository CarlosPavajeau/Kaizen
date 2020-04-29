import { Pipe, PipeTransform } from '@angular/core';
import { Client } from '@app/modules/clients/models/client';

@Pipe({
  name: 'clientsFilter'
})
export class ClientsFilterPipe implements PipeTransform {

  transform(clients: Client[], filter: string): Client[] {
    if (filter) {
      return clients.filter(p => p.lastName.toLowerCase().includes(filter.toLowerCase()) ||
        p.id.includes(filter));
    }
    return null;
  }

}
