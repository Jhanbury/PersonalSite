import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../services/userinfo/userinfo.service';
import { BlogPost } from '../models/blogPost';

@Component({
  selector: 'blog-posts-banner',
  templateUrl: './blog-posts-banner.component.html',
  styleUrls: ['./blog-posts-banner.component.css']
})
export class BlogPostsBannerComponent implements OnInit {
  blogPosts: BlogPost[] = [];
  constructor(private userInfoService:UserInfoService) { }

  ngOnInit() {
    this.userInfoService.getUserBlogPosts(1).subscribe((data) => {
      console.log(data);
      this.blogPosts.push(...data);
    })
  }

  openLink(url:string){
    window.open(url);
  }
  shareLink(post: BlogPost){
    window.open("https://twitter.com/intent/tweet?text=" + post.url + " target='share'");
  }

}
