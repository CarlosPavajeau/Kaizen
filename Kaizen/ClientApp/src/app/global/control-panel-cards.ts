import { DashboardCard } from '@core/models/dashboard-card';

export const DASHBOARS_CARDS: { [role: string]: DashboardCard[] } = {
	Client: [
		{ title: 'Datos de acceso', iconName: 'settings' },
		{ title: 'Mis facturas', iconName: 'attach_money', url: '/payments/service_invoices' },
		{ title: 'Solicitar un servicio', iconName: 'add_circle', url: '/service_requests/register' },
		{ title: 'Mis visitas', iconName: 'info', url: '/activity_schedule/client_schedule' },
		{ title: 'Evaluar servicios', iconName: 'stars' }
	],
	Administrator: [
		{ title: 'Datos de acceso', iconName: 'settings' },
		{ title: 'Facturas', iconName: 'attach_money', url: '/payments/service_invoices' },
		{
			title: 'Gestión de productos',
			iconName: 'label',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar producto', url: '/inventory/products/register', iconName: 'add_circle' },
				{ title: 'Ver productos', url: '/inventory/products', iconName: 'view_list' }
			]
		},
		{
			title: 'Gestión de equipos',
			iconName: 'construction',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar equipo', url: '/inventory/equipments/register', iconName: 'add_circle' },
				{ title: 'Ver equipos', url: '/inventory/equipments', iconName: 'view_list' }
			]
		},
		{
			title: 'Gestión de clientes',
			iconName: 'people',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar cliente', url: '/clients/register', iconName: 'person_add' },
				{ title: 'Ver clientes', url: '/clients', iconName: 'people' },
				{ title: 'Ver solicitudes de clientes', url: '/clients/requests', iconName: 'pending' }
			]
		},
		{
			title: 'Gestión de empleados',
			iconName: 'plumbing',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar empleado', url: '/employees/register', iconName: 'person_add' },
				{ title: 'Ver empleados', url: '/employees', iconName: 'people' }
			]
		},
		{
			title: 'Gestión de servicios',
			iconName: 'batch_prediction',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar servicio', url: '/services/register', iconName: 'add_circle' },
				{ title: 'Ver servicios', url: '/services', iconName: 'view_list' }
			]
		},
		{
			title: 'Calendario',
			iconName: 'event',
			isMenu: true,
			subMenu: [
				{ title: 'Agendar actividad', iconName: 'add_alarm', url: '/activity_schedule/register' },
				{ title: 'Ver actividades', iconName: 'explore', url: '/activity_schedule' },
				{ title: 'Solicitudes de servicio', url: '/service_requests/', iconName: 'pending' }
			]
		}
	],
	TechnicalEmployee: [
		{ title: 'Datos de acceso', iconName: 'settings' },
		{ title: 'Horario de trabajo', iconName: 'calendar_today', url: '/activity_schedule/work_schedule' }
	],
	OfficeEmployee: [
		{ title: 'Datos de acceso', iconName: 'settings' },
		{
			title: 'Gestión de clientes',
			iconName: 'people',
			isMenu: true,
			subMenu: [
				{ title: 'Registrar cliente', url: '/clients/register', iconName: 'person_add' },
				{ title: 'Ver clientes', url: '/clients', iconName: 'people' }
			]
		},
		{ title: 'Solicitudes de clientes', iconName: 'pending', url: '/clients/requests' },
		{ title: 'Solicitudes de servicio', iconName: 'pending', url: '/service_requests' }
	]
};
