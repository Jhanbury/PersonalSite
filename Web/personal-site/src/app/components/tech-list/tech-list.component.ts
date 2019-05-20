import { Component, OnInit, Input } from '@angular/core';
import { Technology } from 'src/app/models/technology';

@Component({
  selector: 'tech-list',
  templateUrl: './tech-list.component.html',
  styleUrls: ['./tech-list.component.scss']
})
export class TechListComponent implements OnInit {
  @Input() technologies: Technology[];
  constructor() { }

  ngOnInit() {
  }

}
