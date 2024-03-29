import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { PipelineComponent } from './pipeline/pipeline.component';
import { authenticatedGuard } from '../account/authenticated.guard';

const routes: Routes = [
  {
    path: 'customers',
    pathMatch: 'full',
    component: CustomerListPageComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'customer/:id',
    pathMatch: 'full',
    component: CustomerDetailComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'pipeline',
    pathMatch: 'full',
    component: PipelineComponent,
    canActivate: [authenticatedGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
