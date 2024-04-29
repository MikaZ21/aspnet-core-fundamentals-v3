import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { Customer } from '../customer.model';

@Component({
  selector: 'crm-customer-list-table',
  templateUrl: './customer-list-table.component.html',
  styleUrls: ['./customer-list-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CustomerListTableComponent {
  displayColumns = ['status-icon', 'name', 'phone', 'email', 'status', 'lastContactDate', 'actions'];

  // Configuring the child component using the @Input decorator in a child component class.
  // e.g. @Input() item = ''; <- decorate the property with @Input()
  // In this case, @Input() decorates the property item, which has a type of string, 
  // however, @Input() properties can have any type, such as number, string, boolean, or object. The value for item comes from the parent component.
  @Input({required: true}) customers!: Customer[] | null
  @Output() openCustomer = new EventEmitter<Customer>();

  openDetail(row: Customer) {
    this.openCustomer.emit(row);
  }
}
