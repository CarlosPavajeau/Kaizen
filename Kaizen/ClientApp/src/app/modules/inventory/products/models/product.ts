import { DataSheet } from '@modules/inventory/products/models/data-sheet';
import { SafetySheet } from '@modules/inventory/products/models/safety-sheet';
import { EmergencyCard } from '@modules/inventory/products/models/emergency-card';

export interface Product {
	code: string;
	healthRegister: string;
	amount: number;
	applicationMoths: number[];
	dataSheet: DataSheet;
	safetySheet: SafetySheet;
	emergencyCard: EmergencyCard;
}
