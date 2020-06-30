import { Moment } from 'moment';
import { Activity } from './activity';

export interface Day {
	name: string;
	number: number;
	isCurrentMonth: boolean;
	isToday: boolean;
	date: Moment;

	activities?: Activity[];
}
