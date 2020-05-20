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
		{
			title: 'Gestionar productos',
			imgUrl: 'assets/images/ecolplag_bird.png',
			url: '/inventory/products/register'
		},
		{
			title: 'Gestionar equipos',
			imgUrl: 'assets/images/ecolplag_bird.png',
			url: '/inventory/equipments/register'
		},
		{ title: 'Gestionar clientes', imgUrl: 'assets/images/ecolplag_bird.png', url: '/clients' },
		{ title: 'Gestionar empleados', imgUrl: 'assets/images/ecolplag_bird.png', url: '/employees' }
	],
	TechnicalEmployee: [],
	OfficeEmployee: []
};
