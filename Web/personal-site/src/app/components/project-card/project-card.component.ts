import { Component, OnInit, Input } from '@angular/core';
import { Project } from 'src/app/models/project';


@Component({
  selector: 'project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.scss']
})
export class ProjectCardComponent implements OnInit {
  @Input() project: Project;
  constructor() { }

  ngOnInit() {
  }

  openLink(args:any){
    console.log(args);
    window.open(args);
  }

}
