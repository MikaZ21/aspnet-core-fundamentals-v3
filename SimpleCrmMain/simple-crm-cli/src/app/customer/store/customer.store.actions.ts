import { createAction, props } from "@ngrx/store";
import { Customer } from "../customer.model";
import { customerSearchCriteria } from "./customer.store.model";
import { Update } from "@ngrx/entity";

// * Actions
export const searchCustomersAction = createAction(
    '[Customer] Search Customers',
    props<{ criteria: customerSearchCriteria }>()
);
export const searchCustomersCompleteAction = createAction(
    '[Customer] Search Customers Completed',
    props<{ result: Customer[] }>()
);
export const addCustomerAction = createAction(
    '[Customer] Add Customer',
    props<{ item: Customer }>()
);
export const addCustomerCompleteAction = createAction(
    '[Customer] Add Customer Completed',
    props<{ result: Customer }>()
);
export const updateCustomerAction = createAction(
    '[Customer] Update Customer',
    props<{ item: Customer }>()
);
export const updateCustomerCompleteAction = createAction(
    '[Customer] Update Customer Completed',
    props<{ result: Update<Customer> }>()
);
  