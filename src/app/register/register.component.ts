import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  public registerForm: FormGroup = new FormGroup({
    username: new FormControl ('', [Validators.required, Validators.minLength (5)], []),
    password: new FormControl ('', [Validators.required], []),
    confirmation: new FormControl ('', [Validators.required], [])
  });

  constructor(){
    this.registerForm.valueChanges.subscribe(console.log)
  }
  Register(){
    console.log("Wurst")
  }
}
