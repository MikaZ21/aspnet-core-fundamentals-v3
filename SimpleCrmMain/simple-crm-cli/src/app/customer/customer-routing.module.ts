import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { PipelineComponent } from './pipeline/pipeline.component';
import { AuthenticatedGuard } from '../account/authenticated.guard';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: CustomerListPageComponent,
    canActivate: [AuthenticatedGuard]
  },
  {
    path: ':id',
    pathMatch: 'full',
    component: CustomerDetailComponent,
    canActivate: [AuthenticatedGuard]
  },
  {
    path: 'pipeline',
    pathMatch: 'full',
    component: PipelineComponent,
    canActivate: [AuthenticatedGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
