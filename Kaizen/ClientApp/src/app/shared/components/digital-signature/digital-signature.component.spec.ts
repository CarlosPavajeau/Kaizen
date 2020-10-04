import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { DigitalSignatureComponent } from './digital-signature.component';

describe('DigitalSignatureComponent', () => {
  let component: DigitalSignatureComponent;
  let fixture: ComponentFixture<DigitalSignatureComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ DigitalSignatureComponent ],
        imports: [ AppModule ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(DigitalSignatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
