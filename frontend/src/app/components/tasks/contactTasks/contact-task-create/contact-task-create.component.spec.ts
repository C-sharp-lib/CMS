import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactTaskCreateComponent } from './contact-task-create.component';

describe('ContactTaskCreateComponent', () => {
  let component: ContactTaskCreateComponent;
  let fixture: ComponentFixture<ContactTaskCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactTaskCreateComponent]
    });
    fixture = TestBed.createComponent(ContactTaskCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
