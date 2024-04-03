import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-quote',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './quote.component.html',
  styleUrl: './quote.component.scss',
})
export class QuoteComponent {
  // Eigenschaften
  public quoteForm: FormGroup = new FormGroup({
    symbol: new FormControl('', [Validators.required], []),
  });

  symbol?: string; //die drei sachen müssen noch angepasst werden.
  name?: string;
  preis?: number;
  fehler?: string;

  // Konstruktor
  constructor(private apiService: ApiService, private router: Router) {}

  // Methoden
  Quote(): void {
    // wenn der knopf gedrückt wurd
    this.fehler = 'Abfrage läuft ...';
    this.apiService
      .Quote({
        Symbol: this.quoteForm.value.symbol,
      })
      .subscribe({
        next: (antwort) => {
          // wenn eine OK antwort kommt
          this.fehler = '';
          console.log(antwort); // ist z.b. {aktienkurs: 34.02}
          this.symbol = antwort.symbol;
          this.name = antwort.name;
          this.preis = antwort.price;
        },
        error: (fehler) => {
          // wenn eine fehler antwort komt
          console.log(fehler);
          this.fehler = 'irgendein fehler';
        },
      });
  }
}
