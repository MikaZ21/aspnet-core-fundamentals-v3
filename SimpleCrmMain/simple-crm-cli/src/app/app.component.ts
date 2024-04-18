import { Component } from '@angular/core';
import { LayoutState, selectShowSideNav, toggleSidenav } from './store/layout.store';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

@Component({
  selector: 'crm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Simple CRM';

  // Components using Store
  // Once store has been defined and imported into the module, the components within that module can now inject and 
  // use the store. This is very similar to other injected dependencies, except that a state type is specified in the angle brackets.
  constructor(private store: Store<LayoutState>) {
    this.showSideNav$ = this.store.pipe(select(selectShowSideNav));
  }

  // Actions can be dispatched from within a component once the Store in injected.
  sideNavToggle() {
    this.store.dispatch(toggleSidenav());
  }

  // Using Selectors
  // The Store injected into a component is an Observable of the intire state tree for the application. 
  // You can use the observable pipe funcion with the NgRx select operator combined with your selector function name to subscribe to the ongoing latest value of that slice of the state.

  showSideNav$: Observable<boolean>;
}
