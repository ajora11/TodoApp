import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FolderListComponent } from './folder-list/folder-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'folders',
    pathMatch: 'full'
  },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'folders', component: FolderListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [FolderListComponent, LoginComponent, RegisterComponent];
