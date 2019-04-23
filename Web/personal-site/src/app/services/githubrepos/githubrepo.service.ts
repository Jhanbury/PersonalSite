import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GithubRepo } from '../../models/githuRepo';

@Injectable({
  providedIn: 'root'
})
export class GithubrepoService {

  constructor(private client: HttpClient) { }

  getGithubRepos(id:number): Observable<GithubRepo[]>{
    return this.client.get<GithubRepo[]>("https://personal-site-api.azurewebsites.net/api/github-repos");
  }
}
