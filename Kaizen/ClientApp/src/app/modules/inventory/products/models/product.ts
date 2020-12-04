import { ApplicationMonths } from '@core/models/months';

export interface Product {
  code: string;
  name: string;
  amount: number;
  presentation: string;
  price: number;
  description: string;
  applicationMonths: ApplicationMonths;
  dataSheet?: string;
  healthRegister?: string;
  safetySheet?: string;
  emergencyCard?: string;
}
