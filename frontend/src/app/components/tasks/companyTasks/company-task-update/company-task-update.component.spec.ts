import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyTaskUpdateComponent } from './company-task-update.component';

describe('CompanyTaskUpdateComponent', () => {
  let component: CompanyTaskUpdateComponent;
  let fixture: ComponentFixture<CompanyTaskUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyTaskUpdateComponent]
    });
    fixture = TestBed.createComponent(CompanyTaskUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
