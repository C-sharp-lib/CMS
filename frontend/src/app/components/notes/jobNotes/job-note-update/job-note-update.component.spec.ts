import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobNoteUpdateComponent } from './job-note-update.component';

describe('JobNoteUpdateComponent', () => {
  let component: JobNoteUpdateComponent;
  let fixture: ComponentFixture<JobNoteUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JobNoteUpdateComponent]
    });
    fixture = TestBed.createComponent(JobNoteUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
