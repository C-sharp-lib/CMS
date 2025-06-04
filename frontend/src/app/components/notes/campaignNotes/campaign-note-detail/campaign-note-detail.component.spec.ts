import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignNoteDetailComponent } from './campaign-note-detail.component';

describe('CampaignNoteDetailComponent', () => {
  let component: CampaignNoteDetailComponent;
  let fixture: ComponentFixture<CampaignNoteDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignNoteDetailComponent]
    });
    fixture = TestBed.createComponent(CampaignNoteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
