import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyNoteListComponent } from './company-note-list.component';

describe('CompanyNoteListComponent', () => {
  let component: CompanyNoteListComponent;
  let fixture: ComponentFixture<CompanyNoteListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyNoteListComponent]
    });
    fixture = TestBed.createComponent(CompanyNoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
