import { BlogPost } from "./blogPost";

export class BlogPostGroup{
    blogs: BlogPost[] = [];
    groupSize: number;

    constructor(blogs:BlogPost[], count:number){
        this.blogs = blogs;
        this.groupSize = count;
    }
}