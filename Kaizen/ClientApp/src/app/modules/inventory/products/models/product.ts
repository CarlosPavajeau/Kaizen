export interface Product {
	code: string;
	name: string;
	amount: number;
	presentation: string;
	price: number;
	description: string;
	applicationMoths?: number;
	dataSheet?: string;
	healthRegister?: string;
	safetySheet?: string;
	emergencyCard?: string;
}
