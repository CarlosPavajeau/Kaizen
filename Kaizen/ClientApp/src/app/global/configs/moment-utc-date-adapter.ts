import { Inject, Injectable, Optional, NgModule } from '@angular/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Moment } from 'moment';
import * as moment from 'moment';
import { MAT_DATE_LOCALE } from '@angular/material/core';

@Injectable()
export class MomentUtcDateAdapter extends MomentDateAdapter {
	constructor(
		@Optional()
		@Inject(MAT_DATE_LOCALE)
		dateLocale: string
	) {
		super(dateLocale);
	}

	createDate(year: number, month: number, date: number): Moment {
		if (month < 0 || month > 11) {
			throw Error(`Invalid month index "${month}". Month index has to be between 0 and 11.`);
		}

		if (date < 1) {
			throw Error(`Invalid date "${date}". Date has to be greater than 0.`);
		}

		const result = moment.utc({ year, month, date }).locale(this.locale);

		if (!result.isValid()) {
			throw Error(`Invalid date "${date}" for month with index "${month}".`);
		}

		return result;
	}
}
