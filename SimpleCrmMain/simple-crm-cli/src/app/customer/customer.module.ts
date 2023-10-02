import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { CustomerService } from './customer.service';
import { CustomerMockService } from './customer-mock.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';

FlexLayoutModule
@NgModule({
  declarations: [
    CustomerListPageComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    CustomerRoutingModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule
  ],
  providers: [
    {
      provide: CustomerService,
      useClass: CustomerMockService
    }
  ]
})

export class CustomerModule { }
