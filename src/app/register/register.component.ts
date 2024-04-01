import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  public registerForm: FormGroup = new FormGroup({
    benutzername: new FormControl('', [Validators.required, Validators.minLength(5)], []),
    passwort: new FormControl('', [Validators.required], []),
    confirmation: new FormControl('', [Validators.required], [])
  });

  constructor(private apiService: ApiService, private router: Router) { }

  registrierungsFehler:boolean = false;

  Register() {

    this.registrierungsFehler=false;
    this.apiService.Register(
      {
        benutzername: this.registerForm.value.benutzername,
        passwort: this.registerForm.value.passwort,
        confirmation: this.registerForm.value.confirmation
      }).subscribe({ next: () => this.hatGeklappt(), error: () => this.hatNichtGeklappt() })
  }

  hatGeklappt() {
    this.router.navigateByUrl("/login")
  }

  hatNichtGeklappt() {
    this.registrierungsFehler=true;
  }
}
