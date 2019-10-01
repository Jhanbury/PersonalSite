import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SocialMediaAccount } from '../../models/socialMediaAccount';
import { User } from '../../models/user';
import { BlogPost } from '../../models/blogPost';
import {environment} from '../../../environments/environment';
import { Hobby } from '../../models/hobby';
import { Project } from 'src/app/models/project';
import { Experience } from 'src/app/models/experience';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor(private client: HttpClient) { }

  getUserSocialAccounts(id:number) : Observable<SocialMediaAccount[]>{
    return this.client.get<Array<SocialMediaAccount>>(environment.baseUrl + `/userinfo/${id}/socialmediaaccounts`);
    
  }
  getUserInfo(id:number): Observable<User>{
    return this.client.get<User>(environment.baseUrl + `/userinfo/${id}`);
  }

  getUserBlogPosts(id:number): Observable<BlogPost[]>{
    return this.client.get<Array<BlogPost>>(environment.baseUrl + `/userinfo/${id}/blogposts`);
  }

  getUserHobbies(id:number): Observable<Hobby[]>{
    return this.client.get<Array<Hobby>>(environment.baseUrl + `/userinfo/${id}/hobbies`);
  }

  getUserProjects(id:number): Observable<Project[]>{
    return this.client.get<Array<Project>>(environment.baseUrl + `/userinfo/${id}/projects`);
  }

  getUserExperience(id:number): Observable<Experience[]>{
    return this.client.get<Array<Experience>>(environment.baseUrl + `/userinfo/${id}/experience`);
  }


}
