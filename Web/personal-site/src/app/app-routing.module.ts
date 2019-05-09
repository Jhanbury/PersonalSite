import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutUserComponent } from './user/about-user/about-user.component';
import { HomePageComponent } from './home-page/home-page.component';

const routes: Routes = [
  { path: 'about-user', component: AboutUserComponent },
  { path: '', component: HomePageComponent }   
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
