import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { MatIconModule } from '@angular/material/icon';
import { LoginComponent } from './login/login.component';
import { SigninMicrosoftComponent } from './signin-microsoft/signin-microsoft.component';
import { SigninGoogleComponent } from './signin-google/signin-google.component';
import { RegistrationComponent } from './registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinner, MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule } from '@angular/common/http';




@NgModule({
  declarations: [NotAuthorizedComponent, 
    LoginComponent, 
    SigninMicrosoftComponent, 
    SigninGoogleComponent, 
    RegistrationComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    NgxSpinnerModule,
  ],
  exports: [
    NgxSpinnerModule,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class AccountModule { }
