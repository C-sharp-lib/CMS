import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleUpdateComponent } from './schedule-update.component';

describe('ScheduleUpdateComponent', () => {
  let component: ScheduleUpdateComponent;
  let fixture: ComponentFixture<ScheduleUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScheduleUpdateComponent]
    });
    fixture = TestBed.createComponent(ScheduleUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
