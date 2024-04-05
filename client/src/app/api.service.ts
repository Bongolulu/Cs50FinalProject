import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, pipe, map, catchError, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://192.168.2.249:3000/';
  private loggedIn = new BehaviorSubject<boolean>(false);
  loggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      this.loggedIn.next(true);
    }
  }

  History(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}history`);
  }

  Portfolio(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`); // Url abrufen
  }

  Register(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}register`, data);
  }

  Quote(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}quote`, data);
  }

  Buy(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}buy`, data);
  }

  Sell(data: any): Observable<any> {
    console.log(data);
    return this.http.post<any>(`${this.apiUrl}sell`, data);
  }

  Login(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}login`, data).pipe(
      map((res: any) => {
        localStorage.setItem('jwtToken', res.token);
        console.log('wir sind jetzt eingeloggt!');
        this.loggedIn.next(true); // hier merken dass man eingeloggt ist
        return true;
      }),
      catchError((err: any) => {
        console.log('innerhalb des pipe kam es zu einem fehler');
        throw false;
      })
    );
  }

  Logout() {
    this.loggedIn.next(false);
    localStorage.removeItem('jwtToken');
  }
}
