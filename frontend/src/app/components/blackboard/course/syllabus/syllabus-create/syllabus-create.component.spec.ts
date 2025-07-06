import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SyllabusCreateComponent } from './syllabus-create.component';

describe('SyllabusCreateComponent', () => {
  let component: SyllabusCreateComponent;
  let fixture: ComponentFixture<SyllabusCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SyllabusCreateComponent]
    });
    fixture = TestBed.createComponent(SyllabusCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
