import React, { useEffect, useState, ComponentType, Dispatch, ReactElement } from 'react';
import { withRouter, RouteComponentProps } from 'react-router';
import { concat, of, Observable, ObservableInput } from 'rxjs';
import { catchError, reduce, switchMap, tap } from 'rxjs/operators';

interface CanActivateState {
  canActivate: boolean;
  isTesting: boolean;
}

interface WithAuthProps extends RouteComponentProps {
  C: ComponentType<any>;
  ownProps: any;
  guards: CanActivateGuard[];
  fallback: ReactElement;
}

export type CanActivateGuard = (route: RouteComponentProps) => Observable<boolean>;

function runGuards(routeProps: RouteComponentProps, guards: CanActivateGuard[], setState: Dispatch<CanActivateState>): () => void {
  // TODO: Support runGuardsAndResolvers.
  const subscription = of(guards)
    .pipe(
      switchMap(
        (): Observable<any> => {
          // Has canActivate guards?
          if (guards.length) {
            // Yes.
            // Run canActivate guards.
            return concat(...guards.map((guard): any => guard(routeProps))).pipe(
              reduce((acc, value): any => acc && value, true),
              catchError(
                (err): ObservableInput<any> => {
                  console.error(err);
                  return of(false);
                }
              )
            );
          } else {
            // No.
            // Bypass canActivate guards execution.
            return of(true);
          }
        }
      ),
      tap((canActivate): void => setState({ canActivate, isTesting: false }))
    )
    .subscribe();
  return subscription.unsubscribe.bind(subscription);
}

// tslint:disable-next-line:variable-name
const WithAuth = withRouter(
  (props: WithAuthProps): ReactElement => {
    const { C, ownProps, guards, fallback } = props;
    const [state, setState] = useState<CanActivateState>({ canActivate: false, isTesting: true });

    useEffect((): void | (() => void | undefined) => runGuards(props, guards, setState), [guards, props]);

    if (state.isTesting) {
      return <></>;
    } else if (state.canActivate) {
      return <C {...ownProps} />;
    } else {
      return fallback;
    }
  }
);

export function withAuth<T extends {}>(
  guards: CanActivateGuard[],
  fallback: ReactElement
): (component: ComponentType<T>) => ComponentType<T> {
  // tslint:disable-next-line:typedef
  return C => ownProps => <WithAuth C={C} ownProps={ownProps} guards={guards} fallback={fallback} />;
}
