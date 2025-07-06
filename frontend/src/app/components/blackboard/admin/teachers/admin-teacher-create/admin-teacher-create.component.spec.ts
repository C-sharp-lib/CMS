import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTeacherCreateComponent } from './admin-teacher-create.component';

describe('AdminTeacherCreateComponent', () => {
  let component: AdminTeacherCreateComponent;
  let fixture: ComponentFixture<AdminTeacherCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminTeacherCreateComponent]
    });
    fixture = TestBed.createComponent(AdminTeacherCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
