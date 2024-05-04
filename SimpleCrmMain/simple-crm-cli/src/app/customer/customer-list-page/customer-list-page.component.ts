import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { Observable, debounceTime, shareReplay, startWith, switchMap, tap } from 'rxjs';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { CustomerState } from '../store/customer.store.model';
import { Store, select } from '@ngrx/store';
import { selectCustomers, selectCriteria } from '../store/customer.store.selectors';
import { searchCustomersAction } from '../store/customer.store.actions';

@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class CustomerListPageComponent implements OnInit {

  filteredCustomers$: Observable<Customer[]>;
  // displayColumns = ['status-icon', 'name', 'phone', 'email', 'status', 'lastContactDate', 'actions'];
  filterInput = new FormControl();
  
  allCustomers$: Observable<Customer[]>;
  searchCriteria!: string;

  constructor(private store: Store<CustomerState>,
              private customerService: CustomerService,
              private router: Router,
              public dialog: MatDialog
              ) {
              this.allCustomers$ = this.store.pipe(select(selectCustomers));
              this.filteredCustomers$ = this.filterInput.valueChanges.pipe(
                startWith(''),
                debounceTime(700),
                tap((filterTerm: string) => {
                  this.searchCustomers(filterTerm);
                }),
                switchMap((filterTerm: string) => {
                  return this.customerService.search(filterTerm);
                }),
                shareReplay(),
              );
  }

  ngOnInit(): void {
    this.store.select(selectCriteria).subscribe(({ term }) => {
      this.searchCriteria = term;
    });
    this.searchCustomers(this.searchCriteria);
  }

  searchCustomers(term: string): void {
    this.store.dispatch(searchCustomersAction({ criteria: { term: term }}))
  }

  openDetail(item: Customer): void {
    if (item) {
      this.router.navigate([`./customer/${item.customerId}`]);
    }
  }

  addCustomer(): void {
    const dialogRef = this.dialog.open(CustomerCreateDialogComponent, {
      width: '250px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // this.store.dispatch(addCustomerAction(result));
        this.customerService.insert(result).subscribe({
          next: cust => { 
            this.store.dispatch(searchCustomersAction({ 
              criteria: { term: this.filterInput.value || '' }} ));
          }
        });
      }
    });
  }

  trackByCustomerId(index: number, item: Customer) {
    return item.customerId;
  }

}

