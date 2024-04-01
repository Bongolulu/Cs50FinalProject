import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-buy',
  standalone: true,
  imports: [],
  templateUrl: './buy.component.html',
  styleUrl: './buy.component.scss'
})
export class BuyComponent {
  symbol: string = "AA" //variable
  preis: number = 5
  
  //Fehlermeldung:
  fehler: string = "Hier kommen später verschiedene Fehler hin, z.B. wenn man wurst eingibt"

  constructor(private apiService: ApiService, private router: Router) { }
}
