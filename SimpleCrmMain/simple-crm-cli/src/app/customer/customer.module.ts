import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { CustomerService } from './customer.service';
import { CustomerMockService } from './customer-mock.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CustomerCreateDialogComponent } from './customer-create-dialog/customer-create-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { StatusIconPipe } from './status-icon.pipe';
import { PipelineComponent } from './pipeline/pipeline.component';
import { EffectsModule } from '@ngrx/effects';
import { CustomerStoreEffects } from './store/customer.store.effects';
import { StoreModule } from '@ngrx/store';
import { customerFeatureKey } from './store/customer.store.selectors';
import { customerReducer } from './store/customer.store.reducer';
@NgModule({
  declarations: [
    CustomerListPageComponent,
    CustomerCreateDialogComponent,
    CustomerDetailComponent,
    StatusIconPipe,
    PipelineComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    CustomerRoutingModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatSnackBarModule,
    EffectsModule.forFeature([CustomerStoreEffects]),
    StoreModule.forFeature(customerFeatureKey, customerReducer),
  ],
  providers: [
    {
      provide: CustomerService,
      // useClass: CustomerMockService
    }
  ]
})

export class CustomerModule { }
