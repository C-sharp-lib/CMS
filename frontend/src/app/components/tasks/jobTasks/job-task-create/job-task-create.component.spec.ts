import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTaskCreateComponent } from './job-task-create.component';

describe('JobTaskCreateComponent', () => {
  let component: JobTaskCreateComponent;
  let fixture: ComponentFixture<JobTaskCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobTaskCreateComponent]
    });
    fixture = TestBed.createComponent(JobTaskCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
