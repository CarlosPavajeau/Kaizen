import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientRequestDetailComponent } from './client-request-detail.component';

describe('ClientRequestDetailComponent', () => {
  let component: ClientRequestDetailComponent;
  let fixture: ComponentFixture<ClientRequestDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientRequestDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientRequestDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
