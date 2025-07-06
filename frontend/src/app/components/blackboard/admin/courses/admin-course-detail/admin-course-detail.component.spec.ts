import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCourseDetailComponent } from './admin-course-detail.component';

describe('AdminCourseDetailComponent', () => {
  let component: AdminCourseDetailComponent;
  let fixture: ComponentFixture<AdminCourseDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminCourseDetailComponent]
    });
    fixture = TestBed.createComponent(AdminCourseDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
