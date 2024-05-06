import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    // keep existing empty route
  {
    path: '',
    redirectTo: 'customers',
    pathMatch: 'full'
  },  
  {
    path: 'customers',
    loadChildren: () => import('./customer/customer.module').then(mod => mod.CustomerModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
