import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactTaskUpdateComponent } from './contact-task-update.component';

describe('ContactTaskUpdateComponent', () => {
  let component: ContactTaskUpdateComponent;
  let fixture: ComponentFixture<ContactTaskUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContactTaskUpdateComponent]
    });
    fixture = TestBed.createComponent(ContactTaskUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
