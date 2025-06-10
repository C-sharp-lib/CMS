import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTaskUpdateComponent } from './user-task-update.component';

describe('UserTaskUpdateComponent', () => {
  let component: UserTaskUpdateComponent;
  let fixture: ComponentFixture<UserTaskUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserTaskUpdateComponent]
    });
    fixture = TestBed.createComponent(UserTaskUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
