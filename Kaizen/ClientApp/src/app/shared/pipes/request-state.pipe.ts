import { Pipe, PipeTransform } from '@angular/core';
import { RequestState } from '@app/modules/service-requests/models/request-state';

@Pipe({
	name: 'requestState'
})
export class RequestStatePipe implements PipeTransform {
	transform(value: RequestState): string {
		switch (value) {
			case RequestState.Accepted:
				return 'Aceptada';
			case RequestState.Pending:
				return 'Pendiente';
			case RequestState.PendingSuggestedDate:
				return 'Pendiente a confirmación de fecha';
			case RequestState.Rejected:
				return 'Cancelada/Rechazada';
		}
	}
}
