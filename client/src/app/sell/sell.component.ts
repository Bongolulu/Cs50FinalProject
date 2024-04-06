import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sell',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './sell.component.html',
  styleUrl: './sell.component.scss',
})
export class SellComponent {
  symbole: string[] = []; //variable  TODO!!!
  public sellForm: FormGroup = new FormGroup({
    symbol: new FormControl('', [Validators.required], []), //Formularfeld
    anzahl: new FormControl(
      '',
      [Validators.required, Validators.pattern('^[1-9][0-9]*$')],
      []
    ), //Formularfeld
  });


  //Fehlermeldung:
  fehler: string =
    'Symbole werden geladen.';

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.apiService.Portfolio().subscribe({
      //Ich rufe die Funktion im api.service.ts ab
      next: (antwort) => {
        // Antwort auf Symbol, Anzahl,... (portfolio)
        this.fehler = ""
        console.log(antwort.bestand);
        this.symbole = antwort.bestand.map((zeile:any) => zeile.symbol);
      },
      error: (fehler) => {
        console.log(fehler);
      },
    });
  }
  Sell(): void {
    // wenn der knopf gedrückt wurde
    this.fehler = 'Abfrage läuft ...';
    this.apiService
      .Sell({
        Symbol: this.sellForm.value.symbol,
        Anzahl: this.sellForm.value.anzahl,
      })
      .subscribe({
        next: (antwort) => {
          this.router.navigateByUrl('/');
          // irgendwann mal checken wieso nur .navigate nicht richtig geht
        },
        error: (antwort: HttpErrorResponse) => {
          if (antwort.status == 404) {
            this.fehler = 'nicht gefunden';
          } else {
            this.fehler = 'Bitte Symbol eingeben!'
          }
          // wenn eine fehler antwort kommt

          console.log(antwort);
        },
      });
  }
}
