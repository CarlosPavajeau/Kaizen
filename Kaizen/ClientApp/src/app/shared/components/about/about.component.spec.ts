import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { AboutComponent } from './about.component';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ AboutComponent ],
        imports: [ AppModule ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
