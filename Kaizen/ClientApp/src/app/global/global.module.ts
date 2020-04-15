import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '../core/material.module';



@NgModule({
  declarations: [],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule
  ]
})
export class GlobalModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: GlobalModule
    };
  }
}
