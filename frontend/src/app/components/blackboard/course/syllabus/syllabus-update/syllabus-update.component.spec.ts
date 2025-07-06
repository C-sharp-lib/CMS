import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SyllabusUpdateComponent } from './syllabus-update.component';

describe('SyllabusUpdateComponent', () => {
  let component: SyllabusUpdateComponent;
  let fixture: ComponentFixture<SyllabusUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SyllabusUpdateComponent]
    });
    fixture = TestBed.createComponent(SyllabusUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
