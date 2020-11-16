import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { TodayStatisticsComponent } from './components/today-statistics/today-statistics.component';
import { StatisticsRoutingModule } from './statistics-routing.module';

@NgModule({
  declarations: [ TodayStatisticsComponent ],
  imports: [ CommonModule, StatisticsRoutingModule, SharedModule ],
  exports: [ TodayStatisticsComponent ]
})
export class StatisticsModule {}
