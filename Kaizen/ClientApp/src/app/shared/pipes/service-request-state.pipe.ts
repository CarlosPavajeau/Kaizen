import { Pipe, PipeTransform } from '@angular/core';
import { ServiceRequestState } from '@app/modules/service-requests/models/service-request-state';

@Pipe({
	name: 'serviceRequestState'
})
export class ServiceRequestStatePipe implements PipeTransform {
	transform(value: ServiceRequestState): string {
		switch (value) {
			case ServiceRequestState.Accepted:
				return 'Aceptada';
			case ServiceRequestState.Pending:
				return 'Pendiente';
			case ServiceRequestState.PendingSuggestedDate:
				return 'Pendiente a confirmación de fecha';
			case ServiceRequestState.Rejected:
				return 'Cancelada/Rechazada';
			default:
				return 'Error, estado de solicitud invalido';
		}
	}
}
