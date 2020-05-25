export interface Product {
	code: string;
	amount: number;
	presentation: string;
	price: number;
	description: string;
	applicationMoths?: number;
	dataSheet?: File;
	healthRegister?: File;
	safetySheet?: File;
	emergencyCard?: File;
}
