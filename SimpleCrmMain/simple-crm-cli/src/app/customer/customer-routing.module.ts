import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { PipelineComponent } from './pipeline/pipeline.component';

const routes: Routes = [
  {
    path: 'customers',
    pathMatch: 'full',
    component: CustomerListPageComponent
  },
  {
    path: 'customer/:id',
    pathMatch: 'full',
    component: CustomerDetailComponent
  },
  {
    path: 'pipeline',
    pathMatch: 'full',
    component: PipelineComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
