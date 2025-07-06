import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleUpdateComponent } from './module-update.component';

describe('ModuleUpdateComponent', () => {
  let component: ModuleUpdateComponent;
  let fixture: ComponentFixture<ModuleUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModuleUpdateComponent]
    });
    fixture = TestBed.createComponent(ModuleUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
