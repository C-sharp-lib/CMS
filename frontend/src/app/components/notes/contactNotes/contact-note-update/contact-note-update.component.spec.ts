import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactNoteUpdateComponent } from './contact-note-update.component';

describe('ContactNoteUpdateComponent', () => {
  let component: ContactNoteUpdateComponent;
  let fixture: ComponentFixture<ContactNoteUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactNoteUpdateComponent]
    });
    fixture = TestBed.createComponent(ContactNoteUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
