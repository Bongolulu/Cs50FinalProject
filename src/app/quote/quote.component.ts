import { Component } from '@angular/core';

@Component({
  selector: 'app-quote',
  standalone: true,
  imports: [],
  templateUrl: './quote.component.html',
  styleUrl: './quote.component.scss'
})
export class QuoteComponent {
symbol =[]
preis: number = 5
fehler: string = "Hier kommen später verschiedene Fehler hin, z.B. wenn man wurst eingibt"
}
