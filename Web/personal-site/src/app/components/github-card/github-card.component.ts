import { Component, OnInit, Input } from '@angular/core';
import { GithubRepo } from 'src/app/models/githuRepo';

@Component({
  selector: 'github-card',
  templateUrl: './github-card.component.html',
  styleUrls: ['./github-card.component.scss']
})
export class GithubCardComponent implements OnInit {
  @Input() repo: GithubRepo;
  constructor() { }

  ngOnInit() {
  }

  openLink(args:any){
    console.log(args);
    window.open(args);
  }

}
