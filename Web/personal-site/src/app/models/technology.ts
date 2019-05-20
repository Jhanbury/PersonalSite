export class Technology {
    id:    number;
    name:  string;
    image: string;

    public constructor(init?:Partial<Technology>) {
        Object.assign(this, init);
    }
}