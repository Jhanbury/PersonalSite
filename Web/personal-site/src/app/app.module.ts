import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ListViewModule } from '@syncfusion/ej2-angular-lists';
import { ToolbarModule } from '@syncfusion/ej2-angular-navigations';
import { RouterModule, Routes} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'; 
import { GithubRepoListComponent } from './components/github-repo-list/github-repo-list.component';
import { UserInfoService } from './services/userinfo/userinfo.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatIconModule, MatListModule,MatToolbarModule, MatCardModule,MatGridListModule, MatMenuModule} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faAdobe } from '@fortawesome/free-brands-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { BlogPostsBannerComponent } from './components/blog-posts-banner/blog-posts-banner.component';
import { AboutUserComponent } from './screens/user/about-user/about-user.component';
import { HomePageComponent } from './screens/home-page/home-page.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { SocialLinksComponent } from './components/social-links/social-links.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { TechListComponent } from './components/tech-list/tech-list.component';
import { ProjectCardComponent } from './components/project-card/project-card.component';
import { SkillListComponent } from './components/skill-list/skill-list.component';
import { UserProjectColumnComponent } from './components/user-project-column/user-project-column.component';
import { GithubCardComponent } from './components/github-card/github-card.component';
library.add(fab, faAdobe);


@NgModule({
  declarations: [
    AppComponent,    
    GithubRepoListComponent,
    BlogPostsBannerComponent,
    AboutUserComponent,
    HomePageComponent,
    SocialLinksComponent,
    NavBarComponent,
    TechListComponent,
    ProjectCardComponent,
    SkillListComponent,
    UserProjectColumnComponent,
    GithubCardComponent
  ],
  imports: [    
    HttpClientModule,
    BrowserModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    FontAwesomeModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    BrowserAnimationsModule,
    ListViewModule, 
    ToolbarModule ,
    AppRoutingModule,
    MDBBootstrapModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports:[FontAwesomeModule]
})
export class AppModule {
  
}
