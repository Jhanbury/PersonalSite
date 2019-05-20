import { Skill } from './skill';
import { Technology } from './technology';

export class Project {
    id:           number;
    userId:       number;
    title:        string;
    description:  string;
    githubRepoId: null;
    projectUrl:   string;
    projectType:  string;
    githubRepo:   null;
    user:         null;
    skills:       Skill[];
    technologies: Technology[];

    public constructor(init?:Partial<Project>) {
        Object.assign(this, init);
    }
}