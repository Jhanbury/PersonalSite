export class Skill {
    id:   number;
    name: string;

    public constructor(init?:Partial<Skill>) {
        Object.assign(this, init);
    }
}