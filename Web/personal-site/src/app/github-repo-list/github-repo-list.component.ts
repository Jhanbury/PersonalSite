import { Component, OnInit } from '@angular/core';
import { GithubRepo } from '../models/githuRepo';
import { GithubrepoService } from '../services/githubrepos/githubrepo.service';
import { User } from '../models/user';


@Component({
  selector: 'github-repo-list',
  templateUrl: './github-repo-list.component.html',
  styleUrls: ['./github-repo-list.component.css']
})
export class GithubRepoListComponent implements OnInit {
  public repos: GithubRepo[] = [];
  public fields: Object = { text: 'name', tooltip: 'Name', id:'id', cssClass:'float:left;' };
  public headertitle = 'Github Repos';
  
  constructor(private githubService:GithubrepoService) { }

  ngOnInit() {
    this.githubService.getGithubRepos(1).subscribe((data) => 
    {
      this.repos.push(...data);
      console.log(this.repos);
    });
    
  }

}
