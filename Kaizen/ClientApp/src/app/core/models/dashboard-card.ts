export interface DashboardCard {
  title: string;
  iconName: string;
  url?: string;
  isMenu?: boolean;
  subMenu?: DashboardCard[];
}
