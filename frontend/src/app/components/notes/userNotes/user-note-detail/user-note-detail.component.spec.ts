import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNoteDetailComponent } from './user-note-detail.component';

describe('UserNoteDetailComponent', () => {
  let component: UserNoteDetailComponent;
  let fixture: ComponentFixture<UserNoteDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserNoteDetailComponent]
    });
    fixture = TestBed.createComponent(UserNoteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
