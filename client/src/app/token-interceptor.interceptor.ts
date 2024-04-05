import { HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mergeMap, switchMap, take } from 'rxjs/operators';
import { catchError, map, Observable, throwError } from "rxjs";
import { ApiService } from './api.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private apiService: ApiService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    if (request.url.includes("login") || request.url.includes("register")) {
      return next.handle(request)
    }
    let token = localStorage.getItem('jwtToken') ?? ""

    const newRequest =  request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })
    
    return next.handle(newRequest);
  }
}