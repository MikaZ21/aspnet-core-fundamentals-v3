import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from './account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private router: Router,
    private snackBar: MatSnackBar,
    private accountSvc: AccountService,
  ) {}

  intercept(
    request: HttpRequest<any>, // information about the outgoing request
    next: HttpHandler // ability to change what it does next if any.
  ): Observable<HttpEvent<any>> {
    const jwtToken = this.getJwtToken(); // Create this function in this class
      // the token is part of the summaryViewModel, don't generate a new token.
      // instead read it from the view model
    if (jwtToken) {
      // use clone here to change the request object and add the header value 
      // you cannot alter the request instance, clone lets you make edits on the copy
      request = request.clone({
        headers: request.headers.set('Authorization', "Bearer " + jwtToken)
      });
      // in the clone options, use intellisense to figure out how to set the 'authorization' header
      // to "Bearer" + logged in user's token.
    }

    return next.handle(request).pipe(
      tap({
        next: (event: HttpEvent<any>) => {},
        error: (err: any) => { // this handles errors in the API Response
          if (err instanceof HttpErrorResponse) {
            // This is where you can check for a 401 or other types of errors
            // use variable 'err' to inspect the error response status code.
            if (err.status === 401) {
              // Do sometihg specific for 401 Unauthorized error
              // For example, redirect the user to the login page
              this.router.navigate(['/login']);
              this.snackBar.open('Not authorized', 'Ok');
            } else {
              // Handle other types of errors here
              // For example, display a generic error message to the user.
              // Also, log the error for debugging purposes.
              console.error('An error occurred: ', err.error.message);
            }
          }
        }
      })
    );
  }
  getJwtToken():string {
    return this.accountSvc.user.value.jwtToken;
  }
}
