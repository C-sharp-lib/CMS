import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTaskDetailComponent } from './job-task-detail.component';

describe('JobTaskDetailComponent', () => {
  let component: JobTaskDetailComponent;
  let fixture: ComponentFixture<JobTaskDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobTaskDetailComponent]
    });
    fixture = TestBed.createComponent(JobTaskDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
