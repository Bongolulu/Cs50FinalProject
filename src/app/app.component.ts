import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SellComponent } from "./sell/sell.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, HeaderComponent, FooterComponent, SellComponent]
})
export class AppComponent {
  title: string = 'financeclient';

  //alarm - app.component.html:
  alarm() {
    alert('Los, klick schon');
  }
}
