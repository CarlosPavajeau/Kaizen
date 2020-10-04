import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { OurservicesComponent } from './ourservices.component';

describe('OurservicesComponent', () => {
  let component: OurservicesComponent;
  let fixture: ComponentFixture<OurservicesComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ OurservicesComponent ],
        imports: [ AppModule ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(OurservicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
