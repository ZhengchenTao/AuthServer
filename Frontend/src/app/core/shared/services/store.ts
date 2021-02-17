import { HelmetProps } from 'react-helmet';
import { applyMiddleware, combineReducers, createStore, Reducer } from 'redux';
import createOidcMiddleware, { loadUser, reducer as oidcReducer, UserState as OidcState } from 'redux-oidc';
import { from, Observable } from 'rxjs';

import { helmetReducer } from '../../redux/helmet/helmet-reducer';
import { urlWatcherReducer, UrlWatcherState } from '../../redux/url-watcher/url-watcher-reducer';

import { userManager } from './user-manager';

const rootReducers: { [P in keyof RootStates]: Reducer<RootStates[P], any> } = {
  helmet: helmetReducer,
  oidc: oidcReducer,
  urlWatcher: urlWatcherReducer
};

const oidcMiddleware = createOidcMiddleware(userManager);

export interface RootStates {
  helmet: HelmetProps;
  oidc: OidcState;
  urlWatcher: UrlWatcherState;
}

export const store = createStore(combineReducers(rootReducers), applyMiddleware(oidcMiddleware));
export const observableStore = from(store as any) as Observable<RootStates>;
loadUser(store, userManager);
