import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { DashboardCardComponent } from './dashboard-card.component';

describe('DashboardCardComponent', () => {
  let component: DashboardCardComponent;
  let fixture: ComponentFixture<DashboardCardComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ DashboardCardComponent ],
        imports: [ AppModule ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCardComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
