import { Moment } from 'moment';
import { Activity } from './activity';

export interface Hour {
	name: string;
	number: number;
	date: Moment;

	activities?: Activity[];
}
