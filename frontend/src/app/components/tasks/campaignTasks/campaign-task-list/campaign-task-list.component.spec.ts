import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignTaskListComponent } from './campaign-task-list.component';

describe('CampaignTaskListComponent', () => {
  let component: CampaignTaskListComponent;
  let fixture: ComponentFixture<CampaignTaskListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignTaskListComponent]
    });
    fixture = TestBed.createComponent(CampaignTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
