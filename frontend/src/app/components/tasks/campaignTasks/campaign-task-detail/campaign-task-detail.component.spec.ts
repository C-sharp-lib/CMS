import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignTaskDetailComponent } from './campaign-task-detail.component';

describe('CampaignTaskDetailComponent', () => {
  let component: CampaignTaskDetailComponent;
  let fixture: ComponentFixture<CampaignTaskDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignTaskDetailComponent]
    });
    fixture = TestBed.createComponent(CampaignTaskDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
