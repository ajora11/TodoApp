import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../data.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
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

    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    this.username = this.registerForm.controls.username.value.trim();
    this.password = this.registerForm.controls.password.value.trim();

    if (this.username === '' || this.password === '') {
      return;
    }

    if (this.registerForm.invalid) {
      return;
    }

    this.dataService.registerUser(this.username, this.password).subscribe(data => {
      if (data != null) {
        this.dataService.changeTokenAndNavigate(data.token, 'folders');
      } else {
        alert ('Registration failed');
      }
    });

  }
}
