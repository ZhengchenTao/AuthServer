import { User, UserManager } from 'oidc-client';
import React, { useEffect, ReactElement } from 'react';
import { withRouter, RouteComponentProps } from 'react-router-dom';
import { CallbackComponent } from 'redux-oidc';

import { localStorageKeyOfLastVisited } from '../shared/services/user-manager';

interface LoginProps extends RouteComponentProps {
  userManager: UserManager;
}

function errorCallback(error: Error): void {
  console.log(error);
}

function successCallback(props: LoginProps, user: User): void {
  const lastVisited = recallLastVisited();
  window.localStorage.setItem('token', user.id_token);
  window.localStorage.setItem('access_token', user.access_token);
  obliviateLastVisited();
  props.history.push(lastVisited);
}

function obliviateLastVisited(): void {
  localStorage.removeItem(localStorageKeyOfLastVisited);
}

function recallLastVisited(): string {
  return localStorage.getItem(localStorageKeyOfLastVisited) || '/';
}

function Login(props: LoginProps): ReactElement {
  useEffect((): void | (() => void | undefined) => {
    if (!props.location.hash) {
      props.userManager.signinRedirect();
    }
  }, [props.location.hash, props.userManager, props.location.pathname]);

  if (!props.location.hash) {
    return <></>;
  } else {
    return (
      <CallbackComponent successCallback={successCallback.bind(null, props)} errorCallback={errorCallback} userManager={props.userManager}>
        <div>正在跳转...</div>
      </CallbackComponent>
    );
  }
}

const loginWithRouter = withRouter(Login);
export { loginWithRouter as Login };
