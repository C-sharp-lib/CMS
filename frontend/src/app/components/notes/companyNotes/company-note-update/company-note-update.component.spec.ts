import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyNoteUpdateComponent } from './company-note-update.component';

describe('CompanyNoteUpdateComponent', () => {
  let component: CompanyNoteUpdateComponent;
  let fixture: ComponentFixture<CompanyNoteUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyNoteUpdateComponent]
    });
    fixture = TestBed.createComponent(CompanyNoteUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
