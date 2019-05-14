import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user';
import { UserInfoService } from '../../../services/userinfo/userinfo.service';
import { Hobby } from '../../../models/hobby';

@Component({
  selector: 'app-about-user',
  templateUrl: './about-user.component.html',
  styleUrls: ['./about-user.component.css']
})
export class AboutUserComponent implements OnInit {
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

  ngOnInit() {
  }

}
