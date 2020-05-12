import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from '@app/app.component';
import { AppModule } from '@app/app.module';

@NgModule({
	imports: [ AppModule, ServerModule, ModuleMapLoaderModule ],
	bootstrap: [ AppComponent ]
})
export class AppServerModule {}
