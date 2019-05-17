import { Component, OnInit } from '@angular/core';
import { UserInfoService } from './services/userinfo/userinfo.service';
import { User } from './models/user';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  
  
  constructor(private userinfoService: UserInfoService){  
      
  } 

  ngOnInit(){
    
  }
}
