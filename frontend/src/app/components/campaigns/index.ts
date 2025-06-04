import {CampaignListComponent} from "./campaign-list/campaign-list.component";
import {CampaignDetailComponent} from "./campaign-detail/campaign-detail.component";
import {CampaignCreateComponent} from "./campaign-create/campaign-create.component";
import {CampaignUpdateComponent} from "./campaign-update/campaign-update.component";


export const components: any[] = [
  CampaignListComponent,
  CampaignDetailComponent,
  CampaignCreateComponent,
  CampaignUpdateComponent,
];

export * from './campaign-list/campaign-list.component';
export * from './campaign-detail/campaign-detail.component';
export * from './campaign-create/campaign-create.component';
export * from './campaign-update/campaign-update.component';
