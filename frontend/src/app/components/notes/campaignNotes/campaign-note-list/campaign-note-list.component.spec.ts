import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignNoteListComponent } from './campaign-note-list.component';

describe('CampaignNoteListComponent', () => {
  let component: CampaignNoteListComponent;
  let fixture: ComponentFixture<CampaignNoteListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CampaignNoteListComponent]
    });
    fixture = TestBed.createComponent(CampaignNoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
