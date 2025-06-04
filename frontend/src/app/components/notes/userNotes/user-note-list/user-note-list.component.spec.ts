import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNoteListComponent } from './user-note-list.component';

describe('UserNoteListComponent', () => {
  let component: UserNoteListComponent;
  let fixture: ComponentFixture<UserNoteListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserNoteListComponent]
    });
    fixture = TestBed.createComponent(UserNoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
