import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;
  success = false;
  username: string;
  password: string;
  token: string;


  constructor(
    private formBuilder: FormBuilder, 
    private dataService: DataService) { 
    this.dataService.currentToken.subscribe(token => this.token = token);
  }

  ngOnInit() {
    if (this.token.length > 0) {
       this.dataService.navigateByUrl('folders');
    }

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    // this.submitted = true;
    
    this.username = this.loginForm.controls.username.value.trim();
    this.password = this.loginForm.controls.password.value.trim();

    if (this.username === '' || this.password === '') {
      return;
    }

    if (this.loginForm.invalid) {
      return;
    }

    this.dataService.signIn(this.username, this.password).subscribe(data => {
      if (data != null) {
        this.dataService.changeTokenAndNavigate(data.token, 'folders');
      } else {
        alert ('Sign in failed');
      }
    });

    // this.success = true;
  }
}
