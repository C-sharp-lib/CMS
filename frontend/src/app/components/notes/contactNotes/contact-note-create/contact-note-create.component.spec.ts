import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactNoteCreateComponent } from './contact-note-create.component';

describe('ContactNoteCreateComponent', () => {
  let component: ContactNoteCreateComponent;
  let fixture: ComponentFixture<ContactNoteCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactNoteCreateComponent]
    });
    fixture = TestBed.createComponent(ContactNoteCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
