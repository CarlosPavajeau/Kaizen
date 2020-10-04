import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { FooterComponent } from './footer.component';

describe('FooterComponent', () => {
  let component: FooterComponent;
  let fixture: ComponentFixture<FooterComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ FooterComponent ],
        imports: [ AppModule ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(FooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
