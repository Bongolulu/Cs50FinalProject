import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Pipe({
  name: 'usdCurrency',
  standalone: true,
})
export class UsdCurrencyPipe implements PipeTransform {
  transform(value: number): string {
    const currencyPipe = new CurrencyPipe('en-US'); // Hier die entsprechende Sprache angeben
    return currencyPipe.transform(value, 'USD', 'symbol', '1.2-2') ?? '';
  }
}
