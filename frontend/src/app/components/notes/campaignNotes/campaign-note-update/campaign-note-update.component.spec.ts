import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignNoteUpdateComponent } from './campaign-note-update.component';

describe('CampaignNoteUpdateComponent', () => {
  let component: CampaignNoteUpdateComponent;
  let fixture: ComponentFixture<CampaignNoteUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignNoteUpdateComponent]
    });
    fixture = TestBed.createComponent(CampaignNoteUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
