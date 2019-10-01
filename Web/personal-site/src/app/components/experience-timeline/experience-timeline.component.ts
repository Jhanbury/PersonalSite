import { Component, OnInit, Input } from '@angular/core';
import { Experience } from 'src/app/models/experience';

@Component({
  selector: 'experience-timeline',
  templateUrl: './experience-timeline.component.html',
  styleUrls: ['./experience-timeline.component.scss']
})
export class ExperienceTimelineComponent implements OnInit {
  @Input() experiences: Experience[] = [];
  constructor() { }

  ngOnInit() {
  }

}
