import { NgModule, Optional, SkipSelf, ModuleWithProviders, LOCALE_ID } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiPrefixInterceptor } from '@core/interceptors/api-prefix.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { CheckClientExistsService } from '@core/services/check-client-exists.service';
import { CheckUserExistsService } from '@core/services/check-user-exists.service';
import { CookieService } from 'ngx-cookie-service';
import { AuthInterceptor } from '@core/interceptors/auth.interceptor';
import { HttpErrorInterceptor } from '@core/interceptors/http-error.interceptor';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { MomentUtcDateAdapter } from '@app/global/configs/moment-utc-date-adapter';
import { MAT_DATE_LOCALE, MAT_DATE_FORMATS, DateAdapter } from '@angular/material/core';
import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import localeEs from '@angular/common/locales/es';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localeEs);

@NgModule({
  declarations: [],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule
  ],
  exports: [ BrowserAnimationsModule, CommonModule ],
  providers: [
    ApiPrefixInterceptor,
    AuthInterceptor,
    HttpErrorInterceptor,
    AuthenticationService,
    CheckClientExistsService,
    CheckUserExistsService,
    CookieService
  ]
})
export class CoreModule {
  constructor(
    @Optional()
    @SkipSelf()
    parentModule: CoreModule
  ) {
    if (parentModule) {
      throw new Error('CoreModule has already been loaded. You should only import Core modules in the AppModule only.');
    }
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [
        { provide: HTTP_INTERCEPTORS, useClass: ApiPrefixInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
        { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 3000 } },
        { provide: MAT_DATE_LOCALE, useValue: 'es-us' },
        { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
        { provide: DateAdapter, useClass: MomentUtcDateAdapter },
        { provide: LOCALE_ID, useValue: 'es' }
      ]
    };
  }
}
