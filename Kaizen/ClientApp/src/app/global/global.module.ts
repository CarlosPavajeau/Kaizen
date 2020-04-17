import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class GlobalModule {
  static forRoot(): ModuleWithProviders<GlobalModule> {
    return {
      ngModule: GlobalModule
    };
  }
}
