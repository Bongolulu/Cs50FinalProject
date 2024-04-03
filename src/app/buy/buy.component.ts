import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-buy',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './buy.component.html',
  styleUrl: './buy.component.scss',
})
export class BuyComponent {
  // Eigenschaften
  public buyForm: FormGroup = new FormGroup({
    symbol: new FormControl('', [Validators.required], []), //Formularfeld
    anzahl: new FormControl(
      '',
      [Validators.required, Validators.pattern('^[0-9]*$')],
      []
    ), //Formularfeld
  });

  //Fehlermeldung:
  fehler: string = '';

  //Konstruktor
  constructor(private apiService: ApiService, private router: Router) {}

  //Methoden
  Buy(): void {
    // wenn der knopf gedrückt wurde
    this.fehler = 'Abfrage läuft ...';
    this.apiService
      .Buy({
        Symbol: this.buyForm.value.symbol,
        Anzahl: this.buyForm.value.anzahl,
      })
      .subscribe({
        next: (antwort) => {
          this.router.navigateByUrl('/');
          // irgendwann mal checken wieso nur .navigate nicht richtig geht
        },
        error: (antwort: HttpErrorResponse) => {
          if (antwort.status == 404) {
            this.fehler = 'nicht gefunden';
          } else {
            this.fehler = 'nicht genug Geld';
          }
          // wenn eine fehler antwort kommt
          // dann ist es entweder 404  (also symbol nicht gefunden), dann ist fehler.status == 404
          // oder ist 400 (nicht genug geld)
          console.log(antwort);
        },
      });
  }
}
