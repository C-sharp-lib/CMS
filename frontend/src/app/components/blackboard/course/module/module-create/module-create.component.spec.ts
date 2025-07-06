import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleCreateComponent } from './module-create.component';

describe('ModuleCreateComponent', () => {
  let component: ModuleCreateComponent;
  let fixture: ComponentFixture<ModuleCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModuleCreateComponent]
    });
    fixture = TestBed.createComponent(ModuleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
