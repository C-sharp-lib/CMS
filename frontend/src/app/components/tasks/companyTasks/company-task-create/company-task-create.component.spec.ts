import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyTaskCreateComponent } from './company-task-create.component';

describe('CompanyTaskCreateComponent', () => {
  let component: CompanyTaskCreateComponent;
  let fixture: ComponentFixture<CompanyTaskCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompanyTaskCreateComponent]
    });
    fixture = TestBed.createComponent(CompanyTaskCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
