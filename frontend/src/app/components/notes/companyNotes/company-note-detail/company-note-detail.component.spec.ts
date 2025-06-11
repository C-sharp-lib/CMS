import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyNoteDetailComponent } from './company-note-detail.component';

describe('CompanyNoteDetailComponent', () => {
  let component: CompanyNoteDetailComponent;
  let fixture: ComponentFixture<CompanyNoteDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyNoteDetailComponent]
    });
    fixture = TestBed.createComponent(CompanyNoteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
