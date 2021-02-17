import mapValues from 'lodash/mapValues';
import React, { useEffect, useState, ComponentType, Dispatch, ReactElement } from 'react';
import { withRouter, RouteComponentProps } from 'react-router';
import { forkJoin, of, Observable, ObservableInput } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';

import { Unpack } from '../../../../types/lib.custom';

interface WithResolverProps extends RouteComponentProps {
  C: ComponentType<any>;
  ownProps: any;
  resolvers: Resolvers;
}

export type Resolver<T = any> = (route: RouteComponentProps) => Observable<T>;

export interface Resolvers {
  [resolverName: string]: Resolver<any>;
}

export interface ResolveProps<T extends Resolvers> {
  resolved: ResolvedData<T>;
}

export type ResolvedData<T extends Resolvers = Resolvers> = { [P in keyof T]: Unpack<ReturnType<T[P]>> };

function runResolvers<T extends Resolvers>(
  props: RouteComponentProps,
  resolvers: T,
  setData: Dispatch<ResolvedData<T> | null>
): () => void {
  // TODO: Support runGuardsAndResolvers.
  const subscription = of([resolvers])
    .pipe(
      switchMap(
        (): ObservableInput<any> => {
          // Has resolvers?
          if (Object.keys(resolvers).length) {
            // Yes.
            // Resolve data.
            const resolverDict: { [resolverName: string]: Observable<any> } = mapValues(
              resolvers,
              (resolver: Resolver): Observable<any> => resolver(props)
            );
            return forkJoin(resolverDict);
          } else {
            // No.
            // Bypass resolvers execution.
            return of({} as ResolvedData<T>);
          }
        }
      ),
      tap(setData)
    )
    .subscribe();
  return subscription.unsubscribe.bind(subscription);
}

// tslint:disable-next-line:variable-name
const WithResolver = withRouter(
  (props: WithResolverProps): ReactElement => {
    const { C, ownProps, resolvers } = props;
    const [data, setData] = useState<ResolvedData | null>(null);
    useEffect((): void | (() => void | undefined) => runResolvers(props, resolvers, setData), [props, resolvers]);

    return !!data ? <C resolved={data} {...ownProps} /> : <></>;
  }
);

export function withResolver<T extends {}, U extends Resolvers>(
  resolvers: U
): (component: ComponentType<T & ResolveProps<U>>) => ComponentType<T> {
  // tslint:disable-next-line:typedef
  return C => ownProps => <WithResolver C={C} ownProps={ownProps} resolvers={resolvers} />;
}
