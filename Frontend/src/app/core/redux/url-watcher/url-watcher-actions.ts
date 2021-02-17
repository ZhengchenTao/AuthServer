export enum UrlWatcherActionTypes {
  FirstRender = '[Url Watcher] FirstRender'
}

export class UrlWatcherFirstRenderAction {
  readonly type = UrlWatcherActionTypes.FirstRender;
  readonly payload = false;

  constructor() {
    return { ...this };
  }
}
