import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CustomerService } from "../customer.service";
import { Injectable } from "@angular/core";
import { map, switchMap } from "rxjs";
import { Customer } from "../customer.model";
import { searchCustomersAction, searchCustomersCompleteAction, updateCustomerAction, updateCustomerCompleteAction } from "./customer.store.actions";

// ngrx effects that trigger all the side effects for specific actions.
@Injectable()
export class CustomerStoreEffects {
    constructor(
        private actions$: Actions, // this event stream is where to listen for all dispatched actions
        private custSvc: CustomerService // this is your service to be called for some actions
    ) {}

    searchCustomers$ = createEffect(() => this.actions$.pipe(
        ofType(searchCustomersAction), 
        // add an rxjs operator to process the action
        switchMap(({criteria}) => // accept action payload
            this.custSvc.search(criteria.term).pipe( // make service call
                map( // create action payload with API response data
                    data => searchCustomersCompleteAction({result: data})
                )
            )
        )
    ))

    updateCustomers$ = createEffect(() =>
        this.actions$.pipe(
            ofType(updateCustomerAction),
            switchMap(({ item }) =>
            this.custSvc.update(item).pipe(
                map((data:Customer) =>
                    updateCustomerCompleteAction({
                        result: {id: data.customerId, changes: { ...data }},
                    }))
            ))
    ))
}