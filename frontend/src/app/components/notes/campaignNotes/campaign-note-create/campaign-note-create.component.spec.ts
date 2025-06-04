import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignNoteCreateComponent } from './campaign-note-create.component';

describe('CampaignNoteCreateComponent', () => {
  let component: CampaignNoteCreateComponent;
  let fixture: ComponentFixture<CampaignNoteCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignNoteCreateComponent]
    });
    fixture = TestBed.createComponent(CampaignNoteCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
