import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobNoteDetailComponent } from './job-note-detail.component';

describe('JobNoteDetailComponent', () => {
  let component: JobNoteDetailComponent;
  let fixture: ComponentFixture<JobNoteDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobNoteDetailComponent]
    });
    fixture = TestBed.createComponent(JobNoteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
