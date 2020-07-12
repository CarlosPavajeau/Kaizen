import { zeroPad } from './number-utils';

export const buildIsoDate = (date: Date, time: any): Date => {
	return new Date(
		`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(date.getDate(), 2)}T${time}:00Z`
	);
};
