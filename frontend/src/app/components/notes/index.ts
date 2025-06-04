import {NoteListComponent} from "./note-list/note-list.component";
import {NoteDetailComponent} from "./note-detail/note-detail.component";
import {NoteCreateComponent} from "./note-create/note-create.component";
import {NoteUpdateComponent} from "./note-update/note-update.component";




export const components: any[] = [
  NoteListComponent,
  NoteDetailComponent,
  NoteCreateComponent,
  NoteUpdateComponent,
];

export * from './note-list/note-list.component';
export * from './note-detail/note-detail.component';
export * from './note-create/note-create.component';
export * from './note-update/note-update.component';
