import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '@app/shared/shared.module';
import { ServiceRequestsModule } from '../../service-requests.module';
import { ServiceRequestProcessComponent } from './service-request-process.component';

describe('ServiceRequestProcessComponent', () => {
  let component: ServiceRequestProcessComponent;
  let fixture: ComponentFixture<ServiceRequestProcessComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule, RouterTestingModule, SharedModule, ServiceRequestsModule ],
        declarations: [ ServiceRequestProcessComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceRequestProcessComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
