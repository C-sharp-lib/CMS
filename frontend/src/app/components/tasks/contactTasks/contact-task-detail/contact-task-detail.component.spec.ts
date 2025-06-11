import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactTaskDetailComponent } from './contact-task-detail.component';

describe('ContactTaskDetailComponent', () => {
  let component: ContactTaskDetailComponent;
  let fixture: ComponentFixture<ContactTaskDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactTaskDetailComponent]
    });
    fixture = TestBed.createComponent(ContactTaskDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
