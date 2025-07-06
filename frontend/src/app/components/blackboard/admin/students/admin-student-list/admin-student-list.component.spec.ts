import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminStudentListComponent } from './admin-student-list.component';

describe('AdminStudentListComponent', () => {
  let component: AdminStudentListComponent;
  let fixture: ComponentFixture<AdminStudentListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminStudentListComponent]
    });
    fixture = TestBed.createComponent(AdminStudentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
