import { NgModule, ModuleWithProviders, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GlobalErrorHandler } from '@global/providers/gobal-error-handler';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class GlobalModule {
  static forRoot(): ModuleWithProviders<GlobalModule> {
    return {
      ngModule: GlobalModule,
      providers: [
        { provide: ErrorHandler, useClass: GlobalErrorHandler },
      ]
    };
  }
}
