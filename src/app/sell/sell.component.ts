import { Component } from '@angular/core';

@Component({
  selector: 'app-sell',
  standalone: true,
  imports: [],
  templateUrl: './sell.component.html',
  styleUrl: './sell.component.scss'
})
export class SellComponent {
  symbole: string[] = ["AA", "TSLA", "AMZN"]  //variable

  //Fehlermeldung:
  fehler: string = "Hier kommen später verschiedene Fehler hin, z.B. wenn man wurst eingibt"
}
