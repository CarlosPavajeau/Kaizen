import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlPanelCardComponent } from './control-panel-card.component';

describe('ControlPanelCardComponent', () => {
  let component: ControlPanelCardComponent;
  let fixture: ComponentFixture<ControlPanelCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControlPanelCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControlPanelCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
