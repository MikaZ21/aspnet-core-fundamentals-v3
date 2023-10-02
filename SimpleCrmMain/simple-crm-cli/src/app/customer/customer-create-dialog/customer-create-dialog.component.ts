import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Customer } from '../customer.model';

@Component({
  selector: 'crm-customer-create-dialog',
  templateUrl: './customer-create-dialog.component.html',
  styleUrls: ['./customer-create-dialog.component.scss']
})
export class CustomerCreateDialogComponent {
  constructor(public dialogRef: MatDialogRef<CustomerCreateDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Customer | null
              ) {
  }

  save(){
    const customer = {};
    this.dialogRef.close(customer);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
