import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { UserComponent } from './auth/user/user.component';
import { HomeComponent } from './home/home.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TaskComponent } from './task/task.component';

const routes: Routes = [

  {path:'',redirectTo:'/user/login',pathMatch:'full'},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },


  {path:'home',component:HomeComponent, canActivate:[AuthGuard],
  children:[
    {path:'', redirectTo:'/home/task', pathMatch:'full'},

    {path:'task', component:TaskComponent, canActivate:[AuthGuard]}
  ]},


  {path:'forbidden',component:ForbiddenComponent},

  {path:'**',component:PageNotFoundComponent},




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
