export enum PeriodicityType {
	Biweekly,
	Monthly,
	BiMonthly,
	Trimester,
	Quarter,
	Quinquemestre,
	Biannual,
	Annual,
	Casual
}

export interface Periodicity {
	name: string;
	type: PeriodicityType;
}

export const PERIODICITIES: Periodicity[] = [
	{ name: 'Quincenal', type: PeriodicityType.Biweekly },
	{ name: 'Mensual', type: PeriodicityType.Monthly },
	{ name: 'BiMensual', type: PeriodicityType.BiMonthly },
	{ name: 'Trimestral', type: PeriodicityType.Trimester },
	{ name: 'Cuatrimestral', type: PeriodicityType.Quarter },
	{ name: 'Quimensual', type: PeriodicityType.Quinquemestre },
	{ name: 'Semestral', type: PeriodicityType.Biannual },
	{ name: 'Anual', type: PeriodicityType.Annual },
	{ name: 'Casual', type: PeriodicityType.Casual }
];
