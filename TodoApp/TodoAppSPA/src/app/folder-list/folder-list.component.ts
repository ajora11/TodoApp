import { Component, OnInit} from '@angular/core';
import { DataService } from '../data.service';


@Component({
  selector: 'app-folder-list',
  templateUrl: './folder-list.component.html',
  styleUrls: ['./folder-list.component.scss']
})
export class FolderListComponent implements OnInit {
  folders: object;
  todos: object = null;
  token: string;
  selectedFolderId: string;
  newFolderName = '';
  newTodoName = '';

  constructor(private dataService: DataService) { 
    this.dataService.currentToken.subscribe(token => this.token = token);
  }

  ngOnInit() {
    if (this.token.length === 0) {
      this.dataService.navigateByUrl('login');
    } else {
      this.getFoldersAndFirstFolderTodos();
    }
  }

  getFolders() {
    this.dataService.getFolders(this.token).subscribe(data => {
      if (data != null) {
        this.folders = data;
      } else {
        // folders couldn't be received
      }
    });
  }

  getFoldersAndFirstFolderTodos() {
    this.dataService.getFolders(this.token).subscribe(data => {
      if (data != null) {
        this.folders = data;
        this.getFolderTodos(data[0].id);
      } else {
        // folders couldn't be received
      }
    });
  }

  getFolderTodos(folderId: string) {
    this.selectedFolderId = folderId;

    this.dataService.getFolderTodos(folderId, this.token).subscribe(data => {
      if (data != null) {
        this.todos = data;
      } else {
        // todos of selected folder couldn't be received
      }
    }); 
  }

  todoDone(todoId: string) {
    this.dataService.todoDone(todoId, this.token).subscribe(data => {
      if (data != null) {
        // data is todo object
        this.getFolderTodos(data.folderId);
      } else {
         // todo done failed
      }
    }); 
  }

  newTodo() {
    if (this.newTodoName.length === 0 || this.newTodoName.length > 25) {
      return;
    }
    this.dataService.addNewTodo(this.newTodoName, this.selectedFolderId, this.token).subscribe(data => {
      if (data != null) {
         this.getFolderTodos(data.folderId);
      } else {
         // adding new folder failed
      }
    }); 
  }

  newFolder() {
    if (this.newFolderName.length === 0 || this.newFolderName.length > 25) {
      return;
    }
    this.dataService.addNewFolder(this.newFolderName, this.token).subscribe(data => {
      if (data != null) {
         this.getFolders();
      } else {
         // adding new folder failed
      }
    }); 
  }

  removeFolder(folderId: string) {
    // to learn folder count
    let size = 0; 
    let key;
    for (key in this.folders) {
        if (this.folders.hasOwnProperty(key)) {
          size++;
        }
    }
    if (size === 1) {
      alert('This is your only list!');  
      return;
    }
    // end

    const r = confirm('Are you sure? All todos will be deleted');
    if (r === false) {
      return;
    }
      
    this.dataService.removeFolder(folderId, this.token).subscribe(data => {
      if (data) { // returns true or false
         if (this.selectedFolderId === folderId) {
          this.getFoldersAndFirstFolderTodos();
         } else {
          this.getFolders();
         }
         
      } else {
         // removing the folder failed
      }
    }); 
  }

  removeTodo(todoId: string) {
    this.dataService.removeTodo(todoId, this.token).subscribe(data => {
      if (data) { // returns true or false
         this.getFolderTodos(this.selectedFolderId);
      } else {
         // removing the todo failed
      }
    }); 
  }


}
