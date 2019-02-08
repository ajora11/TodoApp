import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private tokenSource = new BehaviorSubject(null);
  currentToken = this.tokenSource.asObservable();

  changeToken(token: string) {
    this.tokenSource.next(token);
    this.cookieService.set('token', token); 
  }

  changeTokenAndNavigate(token: string, url: string) {
    this.changeToken(token);
    this.navigateByUrl(url);
  }

  navigateByUrl(url: string) {
    this.router.navigateByUrl(url);
  }

  constructor(private http: HttpClient, private cookieService: CookieService, private router: Router) { 
    if (this.cookieService.check('token')) {
      this.changeToken(this.cookieService.get('token'));
    } else {
      this.changeToken('');
    }
  }

  signIn(username: string, password: string): any {
    return this.http.post('http://localhost:5000/api/auth',
    {
      username,
      password
    });
  }

  registerUser(username: string, password: string): any {
    return this.http.post('http://localhost:5000/api/auth/register',
    {
      username,
      password
    });
  }

  addNewFolder(folderName: string, token: string): any {
    return this.http.get('http://localhost:5000/api/folders/add?folderName=' + folderName, 
    {
      headers: { token } 
    });
  }

  removeFolder(folderId: string, token: string): any {
    return this.http.get('http://localhost:5000/api/folders/remove?folderId=' + folderId, 
    {
      headers: { token } 
    });
  }

  getFolders(token: string): any {
    return this.http.get('http://localhost:5000/api/folders', 
    {
      headers: { token } 
    });
  }

  getFolderTodos(folderId: string, token: string): any {
    return this.http.get('http://localhost:5000/api/todos?folderId=' + folderId, 
    {
      headers: { token } 
    });
  }

  addNewTodo(todoName: string, folderId: string, token: string): any {
    return this.http.get('http://localhost:5000/api/todos/add?todoName=' + todoName + '&folderId=' + folderId, 
    {
      headers: { token } 
    });
  }

  removeTodo(todoId: string, token: string): any {
    return this.http.get('http://localhost:5000/api/todos/remove?todoId=' + todoId, 
    {
      headers: { token } 
    });
  }

  todoDone(todoId: string, token: string): any {
    return this.http.get('http://localhost:5000/api/todos/done?id=' + todoId, 
    {
      headers: { token } 
    });
  }
}
