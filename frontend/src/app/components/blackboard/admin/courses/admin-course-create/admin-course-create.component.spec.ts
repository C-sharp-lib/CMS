import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCourseCreateComponent } from './admin-course-create.component';

describe('AdminCourseCreateComponent', () => {
  let component: AdminCourseCreateComponent;
  let fixture: ComponentFixture<AdminCourseCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminCourseCreateComponent]
    });
    fixture = TestBed.createComponent(AdminCourseCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
