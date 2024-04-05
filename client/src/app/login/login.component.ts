import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public loginForm: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required], []),
    password: new FormControl('', [Validators.required], []),
  });

  constructor(private apiService: ApiService, private router: Router) { }

  loginFehler: boolean = false;

  Login() {

    this.loginFehler = false;
    this.apiService.Login(
      {
        benutzername: this.loginForm.value.username,
        passwort: this.loginForm.value.password
      }).subscribe({ next: (res) => this.hatGeklappt(res), error: (f) => this.hatNichtGeklappt(f) })
  }

  hatGeklappt(res: any) {
    this.router.navigateByUrl("/")
  }

  hatNichtGeklappt(f: any) {
    this.loginFehler = true;
  }
}
