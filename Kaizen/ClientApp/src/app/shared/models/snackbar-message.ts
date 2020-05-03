import { MatSnackBarConfig } from '@angular/material/snack-bar';

export class SnackBarMessage {
	message: string;
	action: string = null;
	config?: MatSnackBarConfig = null;
}
