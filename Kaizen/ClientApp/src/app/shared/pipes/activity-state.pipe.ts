import { Pipe, PipeTransform } from '@angular/core';
import { ActivityState } from '@modules/activity-schedule/models/activity-state';

@Pipe({
	name: 'activityState'
})
export class ActivityStatePipe implements PipeTransform {
	transform(value: ActivityState): string {
		switch (value) {
			case ActivityState.Pending:
				return 'Pendiente';
			case ActivityState.PendingSuggestedDate:
				return 'Pendiente a confirmación de fecha';
			case ActivityState.Accepted:
				return 'Aceptada';
			case ActivityState.Rejected:
				return 'Cancelada/Rechazada';
			case ActivityState.Applied:
				return 'Aplicada';
			default:
				return 'Error, estado de actividad invalido';
		}
	}
}
