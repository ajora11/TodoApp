import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FolderListComponent } from './folder-list/folder-list.component';
import { CookieService } from 'ngx-cookie-service';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from '@angular/forms';



@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      LoginComponent,
      FolderListComponent,
      routingComponents,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      ReactiveFormsModule,
      FormsModule
   ],
   providers: [
      CookieService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
