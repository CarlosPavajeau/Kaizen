import { Pipe, PipeTransform } from '@angular/core';
import { Client } from '@app/modules/clients/models/client';

@Pipe({
  name: 'clientsFilter'
})
export class ClientsFilterPipe implements PipeTransform {

  transform(clients: Client[], filter: string): Client[] {
    if (filter) {
      return clients.filter(p => p.lastName.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
                            p.id.indexOf(filter) !== -1);
    }
    return clients;
  }

}
