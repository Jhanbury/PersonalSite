import { Component, OnInit } from '@angular/core';
import { SocialMediaAccount } from './models/socialMediaAccount';
import { UserInfoService } from './services/userinfo/userinfo.service';
import {faStackOverflow, faTwitter, faGithub, faLinkedin, IconDefinition} from '@fortawesome/free-brands-svg-icons';
import { User } from './models/user';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'John Hanbury';
  socialMediaAccounts: SocialMediaAccount[] = [ {id: 1, accountUrl:'test', platform: 'Facebook'}];
  socialMediaToolbarItems: SocialMediaToolbarItem[] = [];
  public userInfo: User;
  constructor(private userinfoService: UserInfoService){
    
    this.userinfoService.getUserSocialAccounts(1)
      .subscribe((data) => 
      {        
        this.socialMediaToolbarItems = data.map(item => new SocialMediaToolbarItem(item.accountUrl,this.determineIconForPlatform(item.platform)));
        console.log(this.socialMediaToolbarItems);
      });
      this.userinfoService.getUserInfo(1)
      .subscribe((data) =>{
        this.userInfo = data;
      })
  }

  determineIconForPlatform(platform:string): IconDefinition{
    switch(platform){
      case "Stack Overflow":
        return faStackOverflow;
      case "Twitter":
        return faTwitter;
      case "Github":
        return faGithub;
      case "Linkedin":
        return faLinkedin;
    }
  }

  openLink(url:string){
    window.open(url);
  }

  ngOnInit(){
    
  }
}
export class SocialMediaToolbarItem{
  constructor(url:string,icon:IconDefinition){
    this.url = url;
    this.icon = icon;
  }
  url:string;
  icon:IconDefinition;
}