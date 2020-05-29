export enum Months {
	January = 0x00000001,
	February = 0x00000002,
	March = 0x00000004,
	April = 0x00000008,
	May = 0x00000010,
	June = 0x00000020,
	July = 0x00000040,
	August = 0x00000080,
	September = 0x00000100,
	October = 0x00000200,
	November = 0x00000400,
	December = 0x00000800
}

export interface MonthBit {
	name: string;
	value: Months;
}

export const MONTHS: MonthBit[] = [
	{ name: 'Enero', value: Months.January },
	{ name: 'Febrero', value: Months.February },
	{ name: 'Marzo', value: Months.March },
	{ name: 'Abril', value: Months.April },
	{ name: 'Mayo', value: Months.May },
	{ name: 'Junio', value: Months.June },
	{ name: 'Julio', value: Months.July },
	{ name: 'Agosto', value: Months.August },
	{ name: 'Septiembre', value: Months.September },
	{ name: 'Octubre', value: Months.October },
	{ name: 'Noviembre', value: Months.November },
	{ name: 'Diciembre', value: Months.December }
];
