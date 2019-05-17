import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { UserInfoService } from '../../services/userinfo/userinfo.service';
import { Hobby } from '../../models/hobby';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public userInfo: User;
  public userHobbies : Hobby[] = [];
  constructor(private userinfoService: UserInfoService) {
    this.userinfoService.getUserInfo(1)
      .subscribe((data) =>{
        this.userInfo = data;
      });
      this.userinfoService.getUserHobbies(1)
      .subscribe((data) =>{
        this.userHobbies = data;
      });
   }

   scroll(el: HTMLElement) {
    el.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
}

  ngOnInit() {
  }

}
