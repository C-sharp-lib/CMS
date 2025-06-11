import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyTaskListComponent } from './company-task-list.component';

describe('CompanyTaskListComponent', () => {
  let component: CompanyTaskListComponent;
  let fixture: ComponentFixture<CompanyTaskListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyTaskListComponent]
    });
    fixture = TestBed.createComponent(CompanyTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
