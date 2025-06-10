import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactNoteListComponent } from './contact-note-list.component';

describe('ContactNoteListComponent', () => {
  let component: ContactNoteListComponent;
  let fixture: ComponentFixture<ContactNoteListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactNoteListComponent]
    });
    fixture = TestBed.createComponent(ContactNoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
