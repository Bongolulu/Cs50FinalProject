import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { UsdCurrencyPipe } from '../helpers/usd-currency.pipe';

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [FormsModule, UsdCurrencyPipe],
  templateUrl: './portfolio.component.html',
  styleUrl: './portfolio.component.scss',
})
export class PortfolioComponent implements OnInit {
  portfolio: any = [
    {
      symbol: 'A',
      anzahl: 2,
      preis: 12500.23,
      name: 'Limit erreicht',
    },
    {
      symbol: 'AA',
      anzahl: 11,
      preis: 100,
      name: 'Alcoa Corporation',
    },
    {
      symbol: 'FSM',
      anzahl: 3,
      preis: 100,
      name: 'Fortuna Silver Mines Inc.',
    },
    {
      symbol: 'MMM',
      anzahl: 5,
      preis: 100,
      name: '3M Company',
    },
    {
      symbol: 'OVV',
      anzahl: 4,
      preis: 100,
      name: 'Ovintiv Inc.',
    },
    {
      symbol: 'TSLA',
      anzahl: 65,
      preis: 100,
      name: 'Tesla, Inc.',
    },
  ];
  // TODO später any durch richtige typen ersetzen um fehler zu vermeiden
  bargeld: number = 223.32;
  gesamtbetrag: number = 4002.11;

  testwert: any;

  //Fehlermeldung:
  fehler: string = '';

  constructor(private apiService: ApiService) {}

  Sell(index: number) {
    console.log(this.portfolio[index]);
    this.apiService
      .Sell({
        anzahl: this.portfolio[index].tradeanzahl,
        symbol: this.portfolio[index].symbol,
      })
      .subscribe({
        //Ich rufe die Funktion im api.service.ts ab
        next: (antwort) => {
          this.ngOnInit();
        },
        error: (fehler: HttpErrorResponse) => {
          console.log(fehler);
        },
      });
  }

  OkAntwort(antwort: any) {
    // Antwort auf Symbol, Anzahl,... (portfolio)
    console.log(antwort.bestand);
    this.bargeld = antwort.bargeld;
    this.portfolio = antwort.bestand;
    this.gesamtbetrag = antwort.bestand.reduce(
      (summe: any, aktie: any) => summe + aktie.anzahl * aktie.preis,
      antwort.bargeld
    );
  }

  ngOnInit(): void {
    /* this.apiService.Portfolio().subscribe({
      //Ich rufe die Funktion im api.service.ts ab
      next: (antwort) => {
        this.OkAntwort(antwort);
      },
      error: (fehler) => {
        console.log(fehler);
      },
    });*/
  }

  // Methoden:

  Buy(index: number) {
    console.log(this.portfolio[index]);
    this.apiService
      .Buy({
        anzahl: this.portfolio[index].tradeanzahl,
        symbol: this.portfolio[index].symbol,
      })
      .subscribe({
        //Ich rufe die Funktion im api.service.ts ab
        next: (antwort) => {
          this.ngOnInit();
        },
        error: (fehler) => {
          console.log(fehler);
        },
      });
  }
}
