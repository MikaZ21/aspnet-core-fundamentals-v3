import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';


@NgModule({
  declarations: [
    CustomerListPageComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatCardModule,
    MatTableModule
  ]
})
export class CustomerModule { }
