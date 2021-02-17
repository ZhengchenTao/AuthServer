import { AxiosRequestConfig } from 'axios';
import { User } from 'oidc-client';
import React, { useEffect, PropsWithChildren, ReactElement } from 'react';
import Helmet, { HelmetProps } from 'react-helmet';
import { useDispatch, useSelector } from 'react-redux';
import { withRouter, Route, RouteComponentProps, Switch } from 'react-router';

import './app.scss';
import { Login } from './core/components/login';
import { Logout } from './core/components/logout';
import { PrivatePage } from './core/components/private-page';
import { RootEmpty } from './core/components/root-empty';
import { HelmetConfigUpdateAction } from './core/redux/helmet/helmet-actions';
import { UrlWatcherFirstRenderAction } from './core/redux/url-watcher/url-watcher-actions';
import { PRIVATE_MODULES } from './core/shared/data-models/private-module';
import { axios } from './core/shared/services/axios';
import { RootStates } from './core/shared/services/store';
import { localStorageKeyOfLastVisited, userManager } from './core/shared/services/user-manager';
import { ErrorPage } from './shared/components/error-page';

function App(props: PropsWithChildren<RouteComponentProps>): ReactElement {
  const dispatch = useDispatch();
  const isFirstRender = useSelector((state: RootStates): boolean => state.urlWatcher.isFirstRender);
  const helmetConfig = useSelector((state: RootStates): HelmetProps => state.helmet);
  const user = useSelector((state: RootStates): User | undefined => state.oidc.user);

  if (window.localStorage.getItem('token')) {
    axios.interceptors.request.use((config: AxiosRequestConfig): AxiosRequestConfig | Promise<AxiosRequestConfig> => {
      config.headers.post['Content-Type'] = 'application/json';
      config.headers.common['Authorization'] = `Bearer ${window.localStorage.getItem('token')}`;
      return config;
    });
  }

  useEffect(
    (): void | (() => void | undefined) =>
      props.history.listen((location): void => {
        if (isFirstRender) {
          dispatch(new UrlWatcherFirstRenderAction());
        }
        if (location.pathname !== '/login' && location.pathname !== '/logout') {
          localStorage.setItem(localStorageKeyOfLastVisited, location.pathname);
        }
        dispatch(new HelmetConfigUpdateAction(location.pathname));
      }),
    [dispatch, isFirstRender, props.history]
  );

  return (
    <>
      {user?.expired ? (
        <ErrorPage code={401} />
      ) : (
        <>
          <Helmet {...helmetConfig}>
            {helmetConfig.meta}
            {helmetConfig.link}
            {helmetConfig.script}
            {helmetConfig.style}
          </Helmet>
          <Switch>
            <Route path="/login" children={<Login userManager={userManager} />} />
            <Route path="/logout" children={<Logout userManager={userManager} />} />
            <Route path={PRIVATE_MODULES.map((module): string => '/' + module)} component={PrivatePage} />
            <Route path="/" exact={true} component={RootEmpty} />
            <Route path="*" children={<ErrorPage code={404} />} />
          </Switch>
        </>
      )}
    </>
  );
}

const app = withRouter(App);
export { app as App };
