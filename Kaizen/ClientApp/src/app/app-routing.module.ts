import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '@shared/components/home/home.component';
import { Page404Component } from '@shared/components/page404/page404.component';
import { ContactComponent } from '@shared/components/contact/contact.component';
import { AboutComponent } from '@shared/components/about/about.component';
import { OurservicesComponent } from '@shared/components/ourservices/ourservices.component'; 

const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'user', loadChildren: () => import('./modules/users/user.module').then((m) => m.UserModule) },
	{ path: 'clients', loadChildren: () => import('./modules/clients/client.module').then((m) => m.ClientModule) },
	{
		path: 'services',
		loadChildren: () => import('./modules/services/services.module').then((m) => m.ServicesModule)
	},
	{
		path: 'employees',
		loadChildren: () => import('./modules/employees/employees.module').then((m) => m.EmployeesModule)
	},
	{
		path: 'inventory',
		loadChildren: () => import('./modules/inventory/inventory.module').then((m) => m.InventoryModule)
	},
	{
		path: 'calendar',
		loadChildren: () =>
			import('./modules/activity-schedule/activity-schedule.module').then((m) => m.ActivityScheduleModule)
	},
	{
		path: 'payments',
		loadChildren: () => import('./modules/payments/payments.module').then((m) => m.PaymentsModule)
	},
	{
		path: 'work-order',
		loadChildren: () => import('./modules/work-orders/work-orders.module').then((m) => m.WorkOrdersModule)
	},

	{ path: 'contact', component: ContactComponent },
	{ path: 'about', component: AboutComponent },
	{ path: '**', component: Page404Component },
	{ path: 'ourservices', component: OurservicesComponent }
];

@NgModule({
	declarations: [],
	imports: [ RouterModule.forRoot(routes, { enableTracing: false }) ],
	exports: [ RouterModule ]
})
export class AppRoutingModule {}
