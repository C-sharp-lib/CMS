import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactNoteDetailComponent } from './contact-note-detail.component';

describe('ContactNoteDetailComponent', () => {
  let component: ContactNoteDetailComponent;
  let fixture: ComponentFixture<ContactNoteDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactNoteDetailComponent]
    });
    fixture = TestBed.createComponent(ContactNoteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
