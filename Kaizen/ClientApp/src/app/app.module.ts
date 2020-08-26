import { NgModule } from '@angular/core';
import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';
import { CoreModule } from '@core/core.module';
import { GlobalModule } from '@global/global.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [ AppComponent ],
  imports: [ CoreModule.forRoot(), GlobalModule.forRoot(), AppRoutingModule, SharedModule ],
  bootstrap: [ AppComponent ]
})
export class AppModule {}
