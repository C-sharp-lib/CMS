import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentUpdateComponent } from './assignment-update.component';

describe('AssignmentUpdateComponent', () => {
  let component: AssignmentUpdateComponent;
  let fixture: ComponentFixture<AssignmentUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignmentUpdateComponent]
    });
    fixture = TestBed.createComponent(AssignmentUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
