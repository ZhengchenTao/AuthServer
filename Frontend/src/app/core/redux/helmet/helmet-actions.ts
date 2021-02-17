export enum HelmetActionTypes {
  ConfigUpdate = '[Helmet] ConfigUpdate'
}

export class HelmetConfigUpdateAction {
  readonly type = HelmetActionTypes.ConfigUpdate;

  constructor(public readonly payload: string) {
    return { ...this };
  }
}
