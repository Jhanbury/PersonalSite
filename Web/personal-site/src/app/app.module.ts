import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ListViewModule } from '@syncfusion/ej2-angular-lists';
import { ToolbarModule } from '@syncfusion/ej2-angular-navigations';
import { RouterModule, Routes} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'; 
import { GithubRepoListComponent } from './github-repo-list/github-repo-list.component';
import { UserInfoService } from './services/userinfo/userinfo.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatListModule,MatToolbarModule, MatCardModule,MatGridListModule, MatMenuModule} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faAdobe } from '@fortawesome/free-brands-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { BlogPostsBannerComponent } from './blog-posts-banner/blog-posts-banner.component';
import { AboutUserComponent } from './user/about-user/about-user.component';
import { HomePageComponent } from './home-page/home-page.component';
library.add(fab, faAdobe);


@NgModule({
  declarations: [
    AppComponent,    
    GithubRepoListComponent,
    BlogPostsBannerComponent,
    AboutUserComponent,
    HomePageComponent
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
    MatButtonModule,
    BrowserAnimationsModule,
    ListViewModule, 
    ToolbarModule ,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports:[FontAwesomeModule]
})
export class AppModule {
  
}
