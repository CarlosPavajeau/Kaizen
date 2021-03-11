import { Injectable, NgZone } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '@shared/components/error-dialog/error-dialog.component';
import { SuccessDialogComponent } from '@shared/components/success-dialog/success-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogsService {

  constructor(private zone: NgZone, private dialog: MatDialog) {
  }

  showSuccessDialog(message: string, onClose: () => void): void {
    this.zone.run(() => {
      const dialogRef = this.dialog.open(SuccessDialogComponent, {
        width: '500px',
        data: message
      });

      dialogRef.afterClosed().subscribe(() => {
        onClose();
      });
    });
  }

  showErrorDialog(message: string, onClose?: () => void): void {
    this.zone.run(() => {
      const dialogRef = this.dialog.open(ErrorDialogComponent, {
        width: '500px',
        data: message
      });

      dialogRef.afterClosed().subscribe(() => {
        if (onClose) {
          onClose();
        }
      });
    });
  }
}
