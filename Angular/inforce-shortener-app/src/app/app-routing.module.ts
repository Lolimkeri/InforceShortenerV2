import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationComponent } from './authorization/authorization.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { AboutComponent } from './about/about.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: AuthorizationComponent},
  {path: 'about', component: AboutComponent},
  //{path: 'url', component: UrlComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
