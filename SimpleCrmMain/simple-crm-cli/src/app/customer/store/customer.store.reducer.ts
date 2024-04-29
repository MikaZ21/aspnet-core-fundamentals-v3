import { createReducer, on, Action } from "@ngrx/store";
import { searchCustomersAction, searchCustomersCompleteAction, updateCustomerAction, updateCustomerCompleteAction } from "./customer.store.actions";
import { initialCustomerState, customerStateAdapter, CustomerState } from "./customer.store.model";

// * Reducers
const rawCustomerReducer = createReducer(
    initialCustomerState,
    on(searchCustomersAction, (state, action) => ({
      ...state,
      searchStatus: 'searching',
      criteria: action.criteria,
    })),
    on(searchCustomersCompleteAction, (state, action) => {
      return customerStateAdapter.setAll(action.result, {
        ...state,
        searchStatus: 'complete',
      });
    }),
    // on(addCustomerAction, (state,action) => ({
    //   ...state,
    //   addCustStatus: 'adding',
    // })),
    // on(addCustomerCompleteAction, (state, action) => {
    //   return customerStateAdapter.addOne(action.result, {
    //     ...state,
    //     addCustStatus: 'complete',
    //   });
    // }),
    on(updateCustomerAction, (state) => ({
      ...state,
      updateCustomerStatus: 'updating',
    })),
    on(updateCustomerCompleteAction, (state, action) => {
      return customerStateAdapter.updateOne(
        {
          id: action.result.id.toString(),
          changes: { ...action.result.changes },
        },
        {
          ...state,
          updateCustomerStatus: 'complete',
        }
      );
    })
  );
  
  /** Provide reducer in AOT-compilation happy way */
  export function customerReducer(state: CustomerState, action: Action) {
    return rawCustomerReducer(state, action);
  }