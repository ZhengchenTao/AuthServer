import { Observable } from 'rxjs';

export type Omit<T, U> = Pick<T, Exclude<keyof T, U>>;
export type OneOf<T> = { [P in keyof T]: { [K in P]: T[P] } }[keyof T];
export type OneOrAllOf<T> = { [P in keyof T]: { [K in P]: T[P] } }[keyof T] | T;
export type Type<T> = new (...args: any[]) => T;
export type Unpack<T> = T extends Observable<infer U> ? U : any;
