import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CredentialsViewModel, MicrosoftOptions, UserSummaryViewModel, anonymousUser } from './account.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { PlatformLocation } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AccountService { 
  private baseUrl: string;
  private cachedUser = new BehaviorSubject<UserSummaryViewModel>(anonymousUser());
  // UserSummaryViewModel is a new model class to add in 'account.models.ts' 
  // it has properties matching the same named class int the C# project.
  // BefaviorSubject is a type of Observable you can easily set the next value on.
  // Note the one above initializes it to the result of method call anonymousUser()

  constructor(
    private http: HttpClient, // part of Angular to make Http requests
    private router: Router, // part of Angular router, for navigating the user within the app
    private platformLocation: PlatformLocation,
  ){
    this.baseUrl = environment.server + environment.apiUrl + 'auth/'; // Add these properties to environments.ts file
    // the following sets the cached initial user to a blank UserSummary with name 'Anonymous'.
    this.cachedUser.next(anonymousUser()); // This function to be added to 'account.models.ts.
    // You can make up what makes an anonymous user, I usually set the name to 'anonymous' (most users name is an email address)

    const cu = localStorage.getItem('currentUser'); // localStorage is really useful
    if (cu) {
      //if already logged in from before, use that. It has a JWT in it.
      this.cachedUser.next(JSON.parse(cu));
    }
  }

  get user(): BehaviorSubject<UserSummaryViewModel> {
    // components can pipe off of this to get a new value as they login/logout
    return this.cachedUser;
  }

  setUser(user: UserSummaryViewModel): void {
    // called by the components that process a login from password, Google, Microsoft
    this.cachedUser.next(user);
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  // API to load the Options needed to login with Microsoft.
  public loginMicrofoftOptions(): Observable<MicrosoftOptions> {
    return this.http.get<MicrosoftOptions>(
      this.baseUrl + 'external/microsoft'
    );
  }

  // Name and password login API call.
  // If a successful login is completed, you may want to call loginComplete
  // to handle updates to the current user and redirect to where they originally wanted to go.

  public loginPassword(credentials: CredentialsViewModel): Observable<UserSummaryViewModel> {
    this.cachedUser.next(anonymousUser());
    localStorage.removeItem('currentUser');
    return this.http.post<UserSummaryViewModel>(this.baseUrl + 'login', credentials);
  }

  // API to complete the login from a Microsoft login code
  // @param code the login session verification code from Microsoft
  // @param state Any state that needs to be passed around, typically empty/null.

  public loginMicrosoft(code: string, state: string): Observable<UserSummaryViewModel> {
    const body = { accessToken: code, state, baseHref: this.platformLocation.getBaseHrefFromDOM()};
    return this.http.post<UserSummaryViewModel>(this.baseUrl + 'external/microsoft', body);
  }

  public loginGoogle(code: string, state: string): Observable<UserSummaryViewModel> {
    const body = { accessToken: code, state, baseHref: this.platformLocation.getBaseHrefFromDOM()};
    return this.http.post<UserSummaryViewModel>(this.baseUrl + 'external/google', body);
  }

  // Call this to update the current login user information,
  // check they have appropriate access and then send them to their originally requested page (if they have access.)
  // @param data the user data from the login call

  loginComplete(user: UserSummaryViewModel, _message: string) {
   this.setUser(user);
  }

  // API call to cerify a login token is still valid and then refresh the roles they have access to.
  // If the login token is actually expired and this is not called, No Harm is done, 
  // since the API will not return data they no longer have access to.
  // After this successfully returns, please set the user property to the resulting updated user 
  // (don't call loginComplete since that navigates).
  // @param user the user account to verify is still logged in

  public verifyUser(user: UserSummaryViewModel): Observable<UserSummaryViewModel> {
    const model = {};
    const options = !user || !user.jwtToken ? {}
    : { headers : {Authorization: 'Bearer ' + user.jwtToken }};
    return this.http.post<UserSummaryViewModel>(
      this.baseUrl + 'verify',
      model,
      options
    );
  }

  public logout(options: {navigate: boolean} = {navigate: true}): void {
    this.cachedUser.next(anonymousUser());
    localStorage.removeItem('currentUser');
  }
}
