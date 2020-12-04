export enum ApplicationMonths {
  None = 0x0,
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
  value: ApplicationMonths;
}

export const APPLICATION_MONTHS: MonthBit[] = [
  { name: 'Enero', value: ApplicationMonths.January },
  { name: 'Febrero', value: ApplicationMonths.February },
  { name: 'Marzo', value: ApplicationMonths.March },
  { name: 'Abril', value: ApplicationMonths.April },
  { name: 'Mayo', value: ApplicationMonths.May },
  { name: 'Junio', value: ApplicationMonths.June },
  { name: 'Julio', value: ApplicationMonths.July },
  { name: 'Agosto', value: ApplicationMonths.August },
  { name: 'Septiembre', value: ApplicationMonths.September },
  { name: 'Octubre', value: ApplicationMonths.October },
  { name: 'Noviembre', value: ApplicationMonths.November },
  { name: 'Diciembre', value: ApplicationMonths.December }
];
