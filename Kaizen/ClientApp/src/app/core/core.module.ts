import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiPrefixInterceptor } from './interceptors/api-prefix.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { CheckClientExistsService } from '@core/services/check-client-exists.service';
import { CheckUserExistsService } from '@core/services/check-user-exists.service';
import { CookieService } from 'ngx-cookie-service';
import { AuthInterceptor } from '@core/interceptors/auth.interceptor';
import { HttpErrorInterceptor } from '@core/interceptors/http-error.interceptor';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
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
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
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
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
      ]
    };
  }
 }
