import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNoteUpdateComponent } from './user-note-update.component';

describe('UserNoteUpdateComponent', () => {
  let component: UserNoteUpdateComponent;
  let fixture: ComponentFixture<UserNoteUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserNoteUpdateComponent]
    });
    fixture = TestBed.createComponent(UserNoteUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
