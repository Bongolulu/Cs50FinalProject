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
  portfolio = [];
  bargeld: number = 5;
  gesamtwert: number = 4;

  constructor(private apiService: ApiService) {}
  ngOnInit(): void {
    this.apiService.Portfolio().subscribe({ //Ich rufe die Funktion im api.service.ts ab
      next: (antwort) => {
        console.log(antwort);
        this.portfolio = antwort;
      },
      error: (fehler) => {
        console.log(fehler);
      },
    });
  }
}
