import { Component, OnInit } from '@angular/core';
import { SocialMediaAccount } from '../../models/socialMediaAccount';
import { UserInfoService } from '../../services/userinfo/userinfo.service';
import {faStackOverflow, faTwitter, faGithub, faLinkedin, IconDefinition} from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'social-links',
  templateUrl: './social-links.component.html',
  styleUrls: ['./social-links.component.scss']
})
export class SocialLinksComponent implements OnInit {
  socialMediaAccounts: SocialMediaAccount[] = [ {id: 1, accountUrl:'test', platform: 'Facebook'}];
  socialMediaToolbarItems: SocialMediaToolbarItem[] = [];
  constructor(private userinfoService: UserInfoService) { }

  ngOnInit() {
    this.userinfoService.getUserSocialAccounts(1)
      .subscribe((data) => 
      {        
        this.socialMediaToolbarItems = data.map(item => new SocialMediaToolbarItem(item.accountUrl,this.determineIconForPlatform(item.platform),this.determineBtnClassForPlatform(item.platform)));
        console.log(this.socialMediaToolbarItems);
      });
  }

  determineIconForPlatform(platform:string): string{
    switch(platform){
      case "Stack Overflow":
        return "fab fa-stack-overflow menu-item";
      case "Twitter":
        return "fab fa-twitter";
      case "Github":
        return "github";
      case "Linkedin":
        return "fab fa-linkedin-in";
    }
  }

  determineBtnClassForPlatform(platform:string): string{
    switch(platform){
      case "Stack Overflow":
        return "#f48024";
      case "Twitter":
        return "#55ACEE";
      case "Github":
        return "#333";
      case "Linkedin":
        return "#0077b5";
    }
  }
  

  openLink(url:string){
    window.open(url);
  }

}

export class SocialMediaToolbarItem{
  constructor(url:string,icon:string, btnClass:string){
    this.url = url;
    this.icon = icon;
    this.color = btnClass;
  }
  url:string;
  icon:string;
  color:string;
}
