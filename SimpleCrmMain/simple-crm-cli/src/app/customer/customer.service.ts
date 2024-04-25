import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from './customer.model';

@Injectable()
export class CustomerService {

  constructor(private http: HttpClient) { }

  search(term: string): Observable<Customer[]> {
  
    return this.http.get<Customer[]>('/api/customers/?term=' + term);
  }

  get(customerId: number): Observable<Customer> {
    return this.http.get<Customer>('/api/customers/' + customerId);
  }

  insert(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>('/api/customers', customer);
  }

  update(customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(`/api/customers/${customer.customerId}`, customer);
  }
}