import { Pipe, PipeTransform } from '@angular/core';
import { ClientState } from '@modules/clients/models/client-state';

@Pipe({
  name: 'clientState'
})
export class ClientStatePipe implements PipeTransform {
  transform(value: ClientState): string {
    switch (value) {
      case ClientState.Pending:
        return 'Pendiente';
      case ClientState.Acceptep:
        return 'Aceptado';
      case ClientState.Rejected:
        return 'Rechazado';
      case ClientState.Active:
        return 'Activo';
      case ClientState.Casual:
        return 'Casual';
      case ClientState.Inactive:
        return 'Inactivo';
      default:
        return 'Error, estado del cliente invalido';
    }
  }
}
