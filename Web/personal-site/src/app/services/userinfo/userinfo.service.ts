import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SocialMediaAccount } from '../../models/socialMediaAccount';
import { User } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserinfoService {

  constructor(private client: HttpClient) { }

  getUserSocialAccounts(id:number) : Observable<SocialMediaAccount[]>{
    return this.client.get<Array<SocialMediaAccount>>("https://personal-site-api.azurewebsites.net/api/userinfo/1/socialmediaaccounts");
    
  }
  getUserInfo(int:number): Observable<User>{
    return this.client.get<User>("https://personal-site-api.azurewebsites.net/api/userinfo/1");
  }


}
