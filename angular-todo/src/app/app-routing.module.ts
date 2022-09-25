import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { IsAuthenticatedGuard } from './is-authenticated.guard';

const routes: Routes = [
  {path: '', component: TodoListComponent, canActivate: [IsAuthenticatedGuard]},
  {path: 'todo', component: TodoListComponent, canActivate: [IsAuthenticatedGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},

  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
