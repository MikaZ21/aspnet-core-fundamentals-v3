import { EntityAdapter, EntityState, createEntityAdapter } from "@ngrx/entity"
import { Customer } from "../customer.model";

// Defines the state shape/model interface(s) and the initial state (const) value.
export interface CustomerState extends EntityState<Customer> {
    criteria: customerSearchCriteria;
    searchStatus: string;
    addCustomerStatus: string;
    updateCustomerStatus: string;
}

export const customerStateAdapter: EntityAdapter<Customer> = 
    createEntityAdapter<Customer>({
        selectId: (item: Customer) => item.customerId // defines the key property
});

export const initialCustomerState: CustomerState = customerStateAdapter.getInitialState({
    searchStatus: '',
    criteria: {term: ''},
    addCustomerStatus: '',
    updateCustomerStatus: ''
});

export interface customerSearchCriteria {
    term: string;
}