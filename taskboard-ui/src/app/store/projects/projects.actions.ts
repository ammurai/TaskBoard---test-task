import { createAction, props } from '@ngrx/store';
import { Project, ProjectDetail } from '../../core/models/project.model';

export const loadProjects = createAction(
  '[Projects] Load Projects',
  props<{ includeArchived?: boolean }>()
);

export const loadProjectsSuccess = createAction(
  '[Projects] Load Projects Success',
  props<{ projects: Project[] }>()
);

export const loadProjectsFailure = createAction(
  '[Projects] Load Projects Failure',
  props<{ error: string }>()
);

export const loadProject = createAction(
  '[Projects] Load Project',
  props<{ projectId: string }>()
);

export const loadProjectSuccess = createAction(
  '[Projects] Load Project Success',
  props<{ project: ProjectDetail }>()
);

export const loadProjectFailure = createAction(
  '[Projects] Load Project Failure',
  props<{ error: string }>()
);

export const createProject = createAction(
  '[Projects] Create Project',
  props<{ name: string; description?: string | null }>()
);

export const createProjectSuccess = createAction(
  '[Projects] Create Project Success',
  props<{ project: Project }>()
);

export const createProjectFailure = createAction(
  '[Projects] Create Project Failure',
  props<{ error: string }>()
);

export const archiveProject = createAction(
  '[Projects] Archive Project',
  props<{ projectId: string; includeArchived: boolean }>()
);

export const archiveProjectSuccess = createAction('[Projects] Archive Project Success');

export const archiveProjectFailure = createAction(
  '[Projects] Archive Project Failure',
  props<{ error: string }>()
);

export const unarchiveProject = createAction(
  '[Projects] Unarchive Project',
  props<{ projectId: string; includeArchived: boolean }>()
);

export const unarchiveProjectSuccess = createAction('[Projects] Unarchive Project Success');

export const unarchiveProjectFailure = createAction(
  '[Projects] Unarchive Project Failure',
  props<{ error: string }>()
);

export const clearSelectedProject = createAction('[Projects] Clear Selected Project');
