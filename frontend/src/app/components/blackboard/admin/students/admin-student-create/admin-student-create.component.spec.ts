import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminStudentCreateComponent } from './admin-student-create.component';

describe('AdminStudentCreateComponent', () => {
  let component: AdminStudentCreateComponent;
  let fixture: ComponentFixture<AdminStudentCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminStudentCreateComponent]
    });
    fixture = TestBed.createComponent(AdminStudentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
