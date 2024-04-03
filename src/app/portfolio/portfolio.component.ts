import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [],
  templateUrl: './portfolio.component.html',
  styleUrl: './portfolio.component.scss',
})
export class PortfolioComponent implements OnInit {
  portfolio: any = []; // TODO später any durch richtige typen ersetzen um fehler zu vermeiden
  bargeld: number = 0;
  gesamtbetrag: number = 0;

  testwert: any;

  //Fehlermeldung:
  fehler: string = '';

  constructor(private apiService: ApiService) {}

  Test() {
    console.log(this.testwert);
  }

  ngOnInit(): void {
    this.apiService.Portfolio().subscribe({
      //Ich rufe die Funktion im api.service.ts ab
      next: (antwort) => {
        // Antwort auf Symbol, Anzahl,... (portfolio)
        console.log(antwort.bestand);
        this.bargeld = antwort.bargeld;
        this.portfolio = antwort.bestand;
        this.gesamtbetrag = antwort.bestand.reduce(
          (summe: any, aktie: any) => summe + aktie.anzahl * aktie.preis,
          antwort.bargeld
        );
      },
      error: (fehler) => {
        console.log(fehler);
      },
    });
  }

  // Methoden:

  Buy(): void {
    // wenn der knopf gedrückt wurde
    this.fehler = 'Abfrage läuft ...';
    this.apiService
      .Buy({
        Symbol: 'dd',
        Anzahl: 4,
      })
      .subscribe({
        next: (antwort) => {
          // irgendwann mal checken wieso nur .navigate nicht richtig geht
        },
        error: (antwort: HttpErrorResponse) => {
          if (antwort.status == 400) {
            this.fehler = 'nicht genug Geld';
          }
          // wenn eine fehler antwort kommt
          // oder ist 400 (nicht genug geld)
          console.log(antwort);
        },
      });
  }
}
