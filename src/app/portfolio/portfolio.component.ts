import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [],
  templateUrl: './portfolio.component.html',
  styleUrl: './portfolio.component.scss',
})
export class PortfolioComponent implements OnInit {
  portfolio: any = []; // TODO später any durch richtige typen ersetzen um fehler zu vermeiden
  bargeld: number = 5;
  gesamtwert: number = 4;

  constructor(private apiService: ApiService) {}
  ngOnInit(): void {
    this.apiService.Portfolio().subscribe({
      //Ich rufe die Funktion im api.service.ts ab
      next: (antwort) => {
        // Antwort auf Symbol, Anzahl,... (portfolio)
        console.log(antwort.bestand);
        this.portfolio = antwort.bestand;
      },
      error: (fehler) => {
        console.log(fehler);
      },
    });
  }
}
