import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public loginForm: FormGroup = new FormGroup({
    username: new FormControl ('', [Validators.required], []),
    password: new FormControl ('', [Validators.required], []),
  });

  constructor(){
    this.loginForm.valueChanges.subscribe(console.log)
  }
  Login(){
  }
}
