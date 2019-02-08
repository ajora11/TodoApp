import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent {
  title = 'TodoAppSPA';

  constructor() { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
     // alert('test');
  }

}
