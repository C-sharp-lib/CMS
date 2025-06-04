import {TaskListComponent} from "./task-list/task-list.component";
import {TaskDetailComponent} from "./task-detail/task-detail.component";
import {TaskCreateComponent} from "./task-create/task-create.component";
import {TaskUpdateComponent} from "./task-update/task-update.component";


export const components: any[] = [
  TaskListComponent,
  TaskDetailComponent,
  TaskCreateComponent,
  TaskUpdateComponent,
];


export * from './task-list/task-list.component';
export * from './task-detail/task-detail.component';
export * from './task-create/task-create.component';
export * from './task-update/task-update.component';
