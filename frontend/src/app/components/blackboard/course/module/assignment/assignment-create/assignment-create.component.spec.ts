import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentCreateComponent } from './assignment-create.component';

describe('AssignmentCreateComponent', () => {
  let component: AssignmentCreateComponent;
  let fixture: ComponentFixture<AssignmentCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignmentCreateComponent]
    });
    fixture = TestBed.createComponent(AssignmentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
