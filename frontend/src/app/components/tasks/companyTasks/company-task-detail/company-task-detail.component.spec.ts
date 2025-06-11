import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyTaskDetailComponent } from './company-task-detail.component';

describe('CompanyTaskDetailComponent', () => {
  let component: CompanyTaskDetailComponent;
  let fixture: ComponentFixture<CompanyTaskDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyTaskDetailComponent]
    });
    fixture = TestBed.createComponent(CompanyTaskDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
