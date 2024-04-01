import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-quote',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './quote.component.html',
  styleUrl: './quote.component.scss'
})
export class QuoteComponent {
  public quoteForm: FormGroup = new FormGroup({
    symbol: new FormControl('', [Validators.required], []),
  });
symbol =[]
preis: number = 5
fehler: string = "Hier kommen später verschiedene Fehler hin, z.B. wenn man wurst eingibt"

constructor(private apiService: ApiService, private router: Router) { }

Quote(): void {
  this.apiService.Quote({
    Symbol: this.quoteForm.value.symbol
  }).subscribe({
    next: (antwort) => {
      console.log(antwort)
    },
    error: (fehler) => {
      console.log(fehler);
    }
  })
}

}