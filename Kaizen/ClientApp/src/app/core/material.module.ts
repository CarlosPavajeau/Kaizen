import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDividerModule } from '@angular/material/divider';

const MaterialModules = [
	MatButtonModule,
	MatInputModule,
	MatRadioModule,
	MatCardModule,
	MatToolbarModule,
	MatMenuModule,
	MatIconModule,
	MatExpansionModule,
	MatTableModule,
	MatGridListModule,
	MatDialogModule,
	MatCheckboxModule,
	MatChipsModule,
	MatPaginatorModule,
	MatTooltipModule,
	MatSidenavModule,
	MatListModule,
	MatProgressSpinnerModule,
	MatSelectModule,
	MatStepperModule,
	MatSnackBarModule,
	MatSortModule,
	MatSlideToggleModule,
	MatDatepickerModule,
	MatNativeDateModule,
	MatAutocompleteModule,
	MatProgressBarModule,
	MatDividerModule
];

@NgModule({
	exports: [ MaterialModules ]
})
export class MaterialModule {}
