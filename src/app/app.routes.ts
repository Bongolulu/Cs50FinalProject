import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { SellComponent } from './sell/sell.component';
import { LoginComponent } from './login/login.component';
import { BuyComponent } from './buy/buy.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { QuoteComponent } from './quote/quote.component';
import { HistoryComponent } from './history/history.component';

export const routes: Routes = [
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'sell', component: SellComponent},
    {path: 'buy', component: BuyComponent},
    {path: 'portfolio', component: PortfolioComponent},  
    {path: '', redirectTo: '/portfolio', pathMatch: 'full'},
    {path: 'history', component: HistoryComponent},  
    {path: 'quote', component: QuoteComponent},   
]