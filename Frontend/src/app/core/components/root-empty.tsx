import { User } from 'oidc-client';
import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { withRouter, RouteComponentProps } from 'react-router';

import { RootStates } from '../shared/services/store';

function RootEmpty(props: RouteComponentProps): null {
  const user = useSelector((state: RootStates): User | undefined => state.oidc.user);

  useEffect((): void | (() => void | undefined) => props.history.push(user ? '/permission' : '/login'), [props.history, user]);
  return null;
}

const rootEmpty = withRouter(RootEmpty);
export { rootEmpty as RootEmpty };
