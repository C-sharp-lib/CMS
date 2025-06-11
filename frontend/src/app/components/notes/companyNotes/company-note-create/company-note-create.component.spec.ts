import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyNoteCreateComponent } from './company-note-create.component';

describe('CompanyNoteCreateComponent', () => {
  let component: CompanyNoteCreateComponent;
  let fixture: ComponentFixture<CompanyNoteCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyNoteCreateComponent]
    });
    fixture = TestBed.createComponent(CompanyNoteCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
