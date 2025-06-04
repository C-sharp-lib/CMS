import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobNoteCreateComponent } from './job-note-create.component';

describe('JobNoteCreateComponent', () => {
  let component: JobNoteCreateComponent;
  let fixture: ComponentFixture<JobNoteCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobNoteCreateComponent]
    });
    fixture = TestBed.createComponent(JobNoteCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
