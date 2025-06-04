import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNoteCreateComponent } from './user-note-create.component';

describe('UserNoteCreateComponent', () => {
  let component: UserNoteCreateComponent;
  let fixture: ComponentFixture<UserNoteCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserNoteCreateComponent]
    });
    fixture = TestBed.createComponent(UserNoteCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
