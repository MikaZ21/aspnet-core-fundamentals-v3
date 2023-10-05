import { Component, OnInit } from '@angular/core';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.scss'],
})

export class CustomerListPageComponent implements OnInit {

  customers$: Observable<Customer[]>;
  displayColumns = ['name', 'phone', 'email', 'status'];

  constructor(private customerService: CustomerService,
              public dialog: MatDialog
              ) {
              this.customers$ = this.customerService.search('');
  }

  ngOnInit(): void {}

  addCustomer(): void {
    const dialogRef = this.dialog.open(CustomerCreateDialogComponent, {
      width: '250px',
      data: null
    });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log(`The dialog was closed: ${result}`);
    // });
  }
}