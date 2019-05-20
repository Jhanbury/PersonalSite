import { Component, OnInit, Input } from '@angular/core';
import { Skill } from 'src/app/models/skill';

@Component({
  selector: 'skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.scss']
})
export class SkillListComponent implements OnInit {
  @Input() skills: Skill[] = [];
  constructor() { }

  ngOnInit() {
  }

}
