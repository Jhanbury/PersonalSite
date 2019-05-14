import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../../services/userinfo/userinfo.service';
import { BlogPost } from '../../models/blogPost';
import { BlogPostGroup } from '../../models/blogPostGroup';

@Component({
  selector: 'blog-posts-banner',
  templateUrl: './blog-posts-banner.component.html',
  styleUrls: ['./blog-posts-banner.component.css']
})
export class BlogPostsBannerComponent implements OnInit {
  blogPosts: BlogPost[] = [];
  testBlog:any = {id: "5c6ea69c71e13b2fa431b236",  imageUrl: "https://www.bytesizedprogramming.com/content/images/2019/02/IMG_20181111_143017-1.jpg",  teaser: "Welcome to the blog. Let me outline the reasons why Is tarted the blog and what I hope to accomplish",  title: "Welcome to the Blog",  url: "https://www.bytesizedprogramming.com/welcome-to-the-blog/"};
  groups: any = [[]];
  constructor(private userInfoService:UserInfoService) { }

  ngOnInit() {
    this.userInfoService.getUserBlogPosts(1).subscribe((data) => {
      console.log(data);
      this.blogPosts.push(...data);
      // this.CreateGroups(data);
      let blogsTemp = [this.testBlog,this.testBlog,this.testBlog,this.testBlog,this.testBlog,this.testBlog,];
      this.groups = this.chunk(blogsTemp, 3);     
      
    })
  }

  openLink(url:string){
    window.open(url);
  }
  shareLink(post: BlogPost){
    window.open("https://twitter.com/intent/tweet?text=" + post.url + " target='share'");
  }

  chunk(arr, chunkSize) {
    let R = [];
    for (let i = 0, len = arr.length; i < len; i += chunkSize) {
      R.push(arr.slice(i, i + chunkSize));
    }
    return R;
  }
  // CreateGroups(blogs:BlogPost[]){
  //   return this.chunkArray(blogs,3);
  // }

//   chunkArray(myArray :BlogPost[], chunk_size:number){
//     var results: BlogPost[] = [];
//     var groups : BlogPostGroup[] = [];
//     while (myArray.length) {
//         results.push(myArray.splice(0, chunk_size));
//     }
//     //results.push([[{}]])
//     console.log(results);
//     this.groups = results;
// }

}
