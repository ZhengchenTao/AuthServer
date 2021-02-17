import { UrlWatcherActionTypes, UrlWatcherFirstRenderAction } from './url-watcher-actions';

export interface UrlWatcherState {
  isFirstRender: boolean;
}

export function urlWatcherReducer(state: UrlWatcherState = { isFirstRender: true }, action: UrlWatcherFirstRenderAction): UrlWatcherState {
  if (action.type === UrlWatcherActionTypes.FirstRender) {
    return {
      ...state,
      isFirstRender: action.payload
    };
  } else {
    return state;
  }
}
