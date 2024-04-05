import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SellComponent } from "./sell/sell.component";
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, HeaderComponent, FooterComponent, SellComponent,HttpClientModule]
})
export class AppComponent {
  title: string = 'financeclient';

  //alarm - app.component.html:
  alarm() {
    alert('Los, klick schon');
  }
}
