import { Injectable } from '@angular/core';
import { CustomerService } from './customer.service';
import { HttpClient } from '@angular/common/http';
import { Customer } from './customer.model';
import { Observable, of } from 'rxjs';

@Injectable()

export class CustomerMockService extends CustomerService {

  customers: Customer[] = [];
  lastCustomerId: number;


  constructor(http: HttpClient) {
    super(http);
    console.warn('Warning: Using the CustomerMockService, not intended for production use.');

  const localCustomers = localStorage.getItem('customers');
  
  if (localCustomers) {
    this.customers = JSON.parse(localCustomers);
  } else {
    this.customers = [
      {
        customerId: 1,
        firstName: 'John',
        lastName: 'Smith',
        phoneNumber: '314-555-1234',
        emailAddress: 'john@nexulacademy.com',
        statusCode: 'Prospect',
        preferredContactMethod: 'phone',
        lastContactDate: new Date().toISOString()
      },
      {
        customerId: 2,
        firstName: 'Tory',
        lastName: 'Amos',
        phoneNumber: '314-555-9873',
        emailAddress: 'tory@example.com',
        statusCode: 'Prospect',
        preferredContactMethod: 'email',
        lastContactDate: new Date().toISOString()
      },
      {
        customerId: 3,
        firstName: 'Mika',
        lastName: 'Zukeyama',
        phoneNumber: '314-555-0000',
        emailAddress: 'mika@example.com',
        statusCode: 'Prospect',
        preferredContactMethod: 'email',
        lastContactDate: new Date().toISOString()
      }
    ];
  }

  this.lastCustomerId = Math.max(...this.customers.map(x => x.customerId));
  }

  // When searching records, return all items in the customer array that match
  //  the search term. You decide which properties of the customer to match.
  override search(term: string): Observable<Customer[]> {
    const searcResult = this.customers.filter(item => item.lastName.toLocaleLowerCase().startsWith(term.toLocaleLowerCase()));
    return of(searcResult);
  }

  // When inserting a record, add to the customers array (see push), 
  // and then set the new array value into localStorage to ensure the 
  // new values are loaded the next time the page refreshes.
 override insert(customer: Customer): Observable<Customer> {
    customer.customerId = Math.max(...this.customers.map(x => x.customerId)) + 1;
    this.customers.push(customer);
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }

  // When updating a record, replace the prior customer array element with the 
  // updated value, and then set the new array value into localStorage.
 override update(customer: Customer): Observable<Customer> {
    this.customers = this.customers.map((item) => (item.customerId === customer.customerId ? customer: item));
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }

  override get(customerId: number): Observable<Customer> {
    const item = this.customers.find(x => x.customerId === customerId);
    return of(item as Customer);
  }
}
