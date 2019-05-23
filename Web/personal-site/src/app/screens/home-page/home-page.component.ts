import { Component, OnInit, NgZone  } from '@angular/core';
import { User } from '../../models/user';
import { UserInfoService } from '../../services/userinfo/userinfo.service';
import { Hobby } from '../../models/hobby';
import { Technology } from 'src/app/models/technology';
import { Project } from 'src/app/models/project';
import { Skill } from 'src/app/models/skill';
import * as background from '../../../assets/js/background.js';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public projects: Project[] = [
    new Project(
    {
      title: "Personal Site",
      description: "My personal Site",
      projectUrl: "https://google.ie",
      technologies: [new Technology({id:1, name: "Angular", image: "https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/21_Angular_logo_logos-512.png"})],
      skills: [new Skill({id:1, name: "Database Design"}),new Skill({id:2, name: "Web Development"})]
    }),
    new Project(
      {
        title: "GAA API",
        description: "An API which exposes GAA information",
        projectUrl: "https://google.ie",
        technologies: [new Technology({id:1, name: "Angular", image: "https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/21_Angular_logo_logos-512.png"})],
        skills: [new Skill({id:1, name: "Database Design"}),new Skill({id:2, name: "Web Development"})]
      }),
      new Project(
        {
          title: "Reviewer API",
          description: "Review Site for content",
          projectUrl: "https://google.ie",
          technologies: [new Technology({id:1, name: "VueJS", image: "https://upload.wikimedia.org/wikipedia/commons/thumb/9/95/Vue.js_Logo_2.svg/1200px-Vue.js_Logo_2.svg.png"}),
          new Technology({id:1, name: "ASP.NET Core", image: "https://chrissainty.com/content/images/size/w2000/2017/10/aspnet-core.png"})],
          skills: [new Skill({id:1, name: "Database Design"}),new Skill({id:2, name: "Web Development"})]
        }),
  ];
  public userInfo: User;
  public userHobbies : Hobby[] = [];
  public isSideBarVisibile: boolean = true;
  public zone1: NgZone;
  constructor(private userinfoService: UserInfoService,public zone:NgZone ) {   
    this.zone1 = zone;

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
    //window.addEventListener('scroll', this.scrollHandler, true); //third parameter
  }

  ngOnDestroy() {
    //window.removeEventListener('scroll', this.scrollHandler, true);
  }

  handleScroll(event){
    this.zone1.run(() => this.isSideBarVisibile = window.scrollY < 600);
  }

}
