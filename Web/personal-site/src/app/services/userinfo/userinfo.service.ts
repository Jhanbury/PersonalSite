import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SocialMediaAccount } from '../../models/socialMediaAccount';
import { User } from '../../models/user';
import { BlogPost } from '../../models/blogPost';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor(private client: HttpClient) { }

  getUserSocialAccounts(id:number) : Observable<SocialMediaAccount[]>{
    return this.client.get<Array<SocialMediaAccount>>(environment.baseUrl + "/userinfo/1/socialmediaaccounts");
    
  }
  getUserInfo(int:number): Observable<User>{
    return this.client.get<User>(environment.baseUrl + "/userinfo/1");
  }

  getUserBlogPosts(int:number): Observable<BlogPost[]>{
    return this.client.get<Array<BlogPost>>(environment.baseUrl + "/userinfo/1/blogposts");
  }


}
