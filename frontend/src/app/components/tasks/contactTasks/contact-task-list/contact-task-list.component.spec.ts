import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactTaskListComponent } from './contact-task-list.component';

describe('ContactTaskListComponent', () => {
  let component: ContactTaskListComponent;
  let fixture: ComponentFixture<ContactTaskListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactTaskListComponent]
    });
    fixture = TestBed.createComponent(ContactTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
