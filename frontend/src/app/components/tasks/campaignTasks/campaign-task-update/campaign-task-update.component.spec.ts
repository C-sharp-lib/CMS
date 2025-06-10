import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignTaskUpdateComponent } from './campaign-task-update.component';

describe('CampaignTaskUpdateComponent', () => {
  let component: CampaignTaskUpdateComponent;
  let fixture: ComponentFixture<CampaignTaskUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignTaskUpdateComponent]
    });
    fixture = TestBed.createComponent(CampaignTaskUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
