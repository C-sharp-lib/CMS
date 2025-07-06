import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminStudentDetailComponent } from './admin-student-detail.component';

describe('AdminStudentDetailComponent', () => {
  let component: AdminStudentDetailComponent;
  let fixture: ComponentFixture<AdminStudentDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminStudentDetailComponent]
    });
    fixture = TestBed.createComponent(AdminStudentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
