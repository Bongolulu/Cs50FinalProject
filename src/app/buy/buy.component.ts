import { formatCurrency } from '@angular/common';
import { Component } from '@angular/core';

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
}
