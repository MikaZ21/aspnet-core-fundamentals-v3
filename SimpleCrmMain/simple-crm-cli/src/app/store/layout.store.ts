// Defining a State shape (model)
// Each feature area of the application can contain it's own state model.
// store will also require an initial value for the state.

import { Action, createAction, createFeatureSelector, createReducer, createSelector, on } from "@ngrx/store";

export interface LayoutState {
    showSidenav: boolean;
}

const initialState: LayoutState = {
    showSidenav: false
};

export const layoutFeatureKey = 'layout';


// Define Actions
// Actions are simple to define with the built-in ngrx function.
// Each action must have a unique name, application wide. Commonly, the start of the action
// name will include the module name in squre brackets to ensure uniqueness.

export const toggleSidenav = createAction('[Layout] Toggle Sidenav');
export const openSidenav = createAction('[Layout] Open Sidenav');
export const closeSidenav = createAction('[Layout] Close Sidenav');

// Define the Reducer
// 'createReducer' function takes an initial value for this slice of state for this feature module 
// and the collection of reducer functions applicable for each action type.
// Each function for an action must accept the state passed in as the initial state and return a new instance of the same type.
// It should not change the state that was passed in.

// Reducer functions must alter the state syncronously and therefore cannot make a call to an API to retrieve data. 
// If you need to load data asynchronously, use an effect as shown in the next lesson.

const rawLayoutReducer = createReducer(
    initialState,
    on(closeSidenav, state => ({...state, showSidenav: false })),
    on(openSidenav, state => ({...state, showSidenav: true })),
    on(toggleSidenav, state => ({...state, showSidenav: !state.showSidenav }))
);

// Provide reducer in AOT-compilation happy way.
export function layoutReducer(state: LayoutState, action: Action ) {
    return rawLayoutReducer(state, action);
}

// Define Selectors: Read State from the Store
// A Selector is a function that knows how to read a specific slice of the state.
// These pure functions are composed using the createSelector helper function. 
// A special case 'createFeatureSelector' builds the top-level selector for starting at the slice of state for a specific feature.

const getLayoutFeature = createFeatureSelector<LayoutState>(layoutFeatureKey);

export const selectShowSideNav = createSelector(
    getLayoutFeature,
    (state: LayoutState) => state.showSidenav
);