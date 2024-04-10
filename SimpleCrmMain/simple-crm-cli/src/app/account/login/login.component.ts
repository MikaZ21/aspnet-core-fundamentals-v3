import { Component, OnInit } from '@angular/core';
import { UserSummaryViewModel } from '../account.model';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PlatformLocation } from '@angular/common';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'crm-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  loginType: 'undecided' | 'password' | 'microsoft' | 'google' = 'undecided';
  currentStep = 1;
  loginForm: FormGroup;
  loading = false;
  currentUser: Observable<UserSummaryViewModel>;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private platformLocation: PlatformLocation,
    private router: Router,
    private snackBar: MatSnackBar,

  ) {
    this.loginForm = this.fb.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
    this.currentUser = this.accountService.user;
  }

  ngOnInit(): void { 
  }

  useUndecided(): void {
    this.loginType = 'undecided';
  }

  usePassword(): void {
    this.loginType = 'password';
  }

  // This method toggles the display to the first spinner and an option to use another
  // option instead. This is when its time to query your server for the OAuth keys to use,
  // which is better than storing the keys a second time in the Angular source code.
  // Notice this does not get back the private secret key, only the public clientId.

  useMicrosoft(): void {
    this.loginType = 'microsoft';
    this.snackBar.open('Signing in with Microsoft...', '', { duration: 2000 });
    const baseUrl = 
      'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';
    this.accountService.loginMicrofoftOptions().subscribe((opts) => {
      const options: {[key: string]: string } = {
        ...opts,
        response_type: 'code',
        redirect_uri:
          window.location.origin + 
          this.platformLocation.getBaseHrefFromDOM() +
          'account/signin-microsoft',
      };
      console.log(options.redirect_uri);
      let params = new HttpParams();
      for (const key of Object.keys(options)) {
        params = params.set(key, options[key]); //encodes values automatically
      }
      window.location.href = baseUrl + params.toString();
    });
  }

  useGoogle(): void {
    this.loginType = 'google';
    this.snackBar.open('Signing in with Google...', '', { duration: 2000 });
    const baseUrl = 
      'https://accounts.google.com/o/oauth2/v2/auth/oauthchooseaccount?';
    this.accountService.loginMicrofoftOptions().subscribe((opts) => {
      const options: { [key: string]: string } = {
        ...opts,
        response_type: 'code',
        prompt: 'consent',
        access_type: 'offline',
        flowName: 'GeneralOAuthFlow',
        redirect_uri:
          window.location.origin + 
          this.platformLocation.getBaseHrefFromDOM() + 
          'account/signin-google',
      };
      console.log(options.redirect_uri);
      let params = new HttpParams();
      for (const key of Object.keys(options)) {
        params = params.set(key, options[key]); //encode values automatically.
      }
      window.location.href = baseUrl + params.toString();
    });
  }

  onSubmit(): void {
    if (!this.loginForm.valid) {
      return;
    }
    this.loading = true;
    const creds = { ...this.loginForm.value };
    this.accountService.loginPassword(creds).subscribe({
      next: result => {
        this.accountService.loginComplete(result, 'Login Complete');
      },
      error: _ => { //_ is an error, interceptor shows snackbar based on api response
        this.loading = false;
      }
    });
  }

  register(): void {
    this.router.navigate(['./register']);
  }
}
