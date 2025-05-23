import {ContactListComponent} from "./contact-list/contact-list.component";
import {ContactCreateComponent} from "./contact-create/contact-create.component";
import {ContactDetailComponent} from "./contact-detail/contact-detail.component";
import {ContactUpdateComponent} from "./contact-update/contact-update.component";


export const components: any[] = [
  ContactListComponent,
  ContactCreateComponent,
  ContactDetailComponent,
  ContactUpdateComponent,
];

export * from './contact-list/contact-list.component';
export * from './contact-detail/contact-detail.component';
export * from './contact-create/contact-create.component';
export * from './contact-update/contact-update.component';

