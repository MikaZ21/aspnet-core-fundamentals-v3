import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Customer } from '../customer.model';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'crm-customer-create-dialog',
  templateUrl: './customer-create-dialog.component.html',
  styleUrls: ['./customer-create-dialog.component.scss'],
})

export class CustomerCreateDialogComponent {
  detailForm: FormGroup;

  constructor(
              private fb: FormBuilder,
              public dialogRef: MatDialogRef<CustomerCreateDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Customer | null
              ) {
                this.detailForm = this.fb.group({
                  firstName: ['', Validators.required],
                  lastName: ['', Validators.required], 
                  phoneNumber: [''],
                  emailAddress: ['', [Validators.required, Validators.email]],
                  preferredContactMethod: ['']
                });
                if (this.data) {
                  this.detailForm.patchValue(this.data);
                }
  }

  ngOnInit(): void {  
  }

  save(): void {
    if (!this.detailForm.valid) {
      
      return;
    }
    const customer = { ...this.data, ...this.detailForm.value };
    this.dialogRef.close(customer);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
