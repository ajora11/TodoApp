import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  token: string;
  appTitle = 'todo App';
  loggedIn = false;

  constructor(
    private dataService: DataService) { 
      this.dataService.currentToken.subscribe(token => this.token = token);
  }

  ngOnInit() {
     // this.dataService.changeToken('');
  }  
  
  signOut() {
    this.dataService.changeTokenAndNavigate('', 'login');
  }

}
