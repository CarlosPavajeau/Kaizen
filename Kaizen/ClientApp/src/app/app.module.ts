import { NgModule } from '@angular/core';

import { AppComponent } from '@app/app.component';
import { AppRoutingModule } from '@app/app-routing.module';
import { CoreModule } from '@core/core.module';
import { GlobalModule } from '@global/global.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [ AppComponent ],
  imports: [ CoreModule.forRoot(), GlobalModule.forRoot(), AppRoutingModule, SharedModule ],
  providers: [],
  bootstrap: [ AppComponent ]
})
export class AppModule {}
