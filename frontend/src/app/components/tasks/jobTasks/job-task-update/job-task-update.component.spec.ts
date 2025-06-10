import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTaskUpdateComponent } from './job-task-update.component';

describe('JobTaskUpdateComponent', () => {
  let component: JobTaskUpdateComponent;
  let fixture: ComponentFixture<JobTaskUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobTaskUpdateComponent]
    });
    fixture = TestBed.createComponent(JobTaskUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
