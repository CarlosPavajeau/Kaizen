import { ControlPanelCard } from '@app/core/models/control-panel-card';

export const CONTROL_PANEL_CARDS: { [role: string]: ControlPanelCard[] } = {
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
