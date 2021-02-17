export interface SharedDataResponse {
  code: number;
  msg: string;
}

export interface SharedDataListModel<T> extends SharedDataResponse {
  data: {
    list: T[];
    total: number;
  };
}

export interface SharedDataModel<T> extends SharedDataResponse {
  data: T;
}

export interface SharedFieldModel {
  id: string;
  createdBy?: string;
  createdByName?: string;
  lastUpdatedByName?: string;
  lastUpdatedBy?: string;
  createdTime?: string;
  lastUpdatedTime?: string;
}
