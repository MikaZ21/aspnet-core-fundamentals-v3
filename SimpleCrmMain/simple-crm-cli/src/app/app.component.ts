import { Component } from '@angular/core';
import { LayoutState, toggleSidenav } from './store/layout.store';
import { Store } from '@ngrx/store';

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
  constructor(private store: Store<LayoutState>) {}

  // Actions can be dispatched from within a component once the Store in injected.
  sideNavToggle() {
    this.store.dispatch(toggleSidenav());
  }
}
