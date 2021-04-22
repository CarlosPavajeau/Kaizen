import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardInfoItemComponent } from './card-info-item.component';

describe('CardInfoItemComponent', () => {
  let component: CardInfoItemComponent;
  let fixture: ComponentFixture<CardInfoItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardInfoItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardInfoItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
