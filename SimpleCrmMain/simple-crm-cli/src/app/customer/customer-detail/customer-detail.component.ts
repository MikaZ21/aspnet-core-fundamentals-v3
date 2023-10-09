import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})

export class CustomerDetailComponent implements OnInit {
  customerId: number;
  customer: Observable<Customer>;
  detailForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private customerService: CustomerService) {
      this.customerId = +this.route.snapshot.params['id'];
      this.customer = this.customerService.get(this.customerId);  
      this.createForm();  
    }

    public createForm(): void {
                this.detailForm = this.fb.group({
                  firstName: ['', Validators.required],
                  lastName: ['', Validators.required], 
                  phoneNumber: [''],
                  emailAddress: ['', [Validators.required, Validators.email]],
                  preferredContactMethod: ['email']
                });
  }

  ngOnInit(): void {
  }
}