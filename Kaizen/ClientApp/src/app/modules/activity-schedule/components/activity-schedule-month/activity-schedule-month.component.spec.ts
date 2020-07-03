import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityScheduleMonthComponent } from './activity-schedule-month.component';

describe('ActivityScehduleMonthComponent', () => {
	let component: ActivityScheduleMonthComponent;
	let fixture: ComponentFixture<ActivityScheduleMonthComponent>;

	beforeEach(
		async(() => {
			TestBed.configureTestingModule({
				declarations: [ ActivityScheduleMonthComponent ]
			}).compileComponents();
		})
	);

	beforeEach(() => {
		fixture = TestBed.createComponent(ActivityScheduleMonthComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
