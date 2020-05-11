import { DashboardCard } from '@core/models/dashboard-card';

export const DASHBOARS_CARDS: { [role: string]: DashboardCard[] } = {
	Client: [
		{ title: 'Datos de acceso', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Mis facturas', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Solicitar un servicio', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Gestionar visitas', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Gestionar visitas', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' }
	],
	Administrator: [
		{ title: 'Datos de acceso', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Mis facturas', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Solicitar un servicio', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' },
		{ title: 'Gestionar empleados', imgUrl: 'assets/images/ecolplag_bird.png', url: '/services' }
	],
	TechnicalEmployee: [],
	OfficeEmployee: []
};
