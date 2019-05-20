import { Component, OnInit, Input } from '@angular/core';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'user-project-column',
  templateUrl: './user-project-column.component.html',
  styleUrls: ['./user-project-column.component.scss']
})
export class UserProjectColumnComponent implements OnInit {
  @Input() projects: Project[];
  constructor() { }

  ngOnInit() {
  }

}
