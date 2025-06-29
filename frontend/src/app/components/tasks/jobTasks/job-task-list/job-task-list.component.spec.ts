import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTaskListComponent } from './job-task-list.component';

describe('JobTaskListComponent', () => {
  let component: JobTaskListComponent;
  let fixture: ComponentFixture<JobTaskListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobTaskListComponent]
    });
    fixture = TestBed.createComponent(JobTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
