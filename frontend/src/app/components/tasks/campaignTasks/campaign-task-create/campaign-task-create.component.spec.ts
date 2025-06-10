import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignTaskCreateComponent } from './campaign-task-create.component';

describe('CampaignTaskCreateComponent', () => {
  let component: CampaignTaskCreateComponent;
  let fixture: ComponentFixture<CampaignTaskCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignTaskCreateComponent]
    });
    fixture = TestBed.createComponent(CampaignTaskCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
