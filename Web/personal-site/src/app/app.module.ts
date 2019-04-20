import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ListViewModule } from '@syncfusion/ej2-angular-lists';
import { ToolbarModule } from '@syncfusion/ej2-angular-navigations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'; 
import { GithubRepoListComponent } from './github-repo-list/github-repo-list.component';
import { UserinfoService } from './services/userinfo/userinfo.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatListModule,MatToolbarModule} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faAdobe } from '@fortawesome/free-brands-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
library.add(fab, faAdobe);
@NgModule({
  declarations: [
    AppComponent,    
    GithubRepoListComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    MatListModule,
    FontAwesomeModule,
    MatToolbarModule,
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
