import { Button, Result } from 'antd';
import React, { useEffect, useState, ReactElement } from 'react';
import { useSelector } from 'react-redux';
import { useHistory } from 'react-router';

import { RootStates } from '../../core/shared/services/store';
import { HttpStatusCode } from '../data-models/http-status-code';

export function ErrorPage(props: { code: HttpStatusCode }): ReactElement | null {
  const history = useHistory();
  const isFirstRender = useSelector((state: RootStates): boolean => state.urlWatcher.isFirstRender);
  const [canShow, setCanShow] = useState<Boolean>(false);
  let message = '';
  switch (props.code) {
    case HttpStatusCode.Unauthorized:
      message = 'Oops...请重新登录。';
      break;
    case HttpStatusCode.Forbidden:
      message = 'Oops...你没有权限访问这个页面。';
      break;
    case HttpStatusCode.NotFound:
      message = 'Oops...你想访问的页面不存在。';
      break;
    default:
      message = 'Oops...发生了未知错误。';
  }

  const btnClick = (code: HttpStatusCode): void => {
    if (code === 401) {
      history.push('/login');
    } else {
      history.push('/permission');
    }
  };

  useEffect((): void | (() => void | undefined) => {
    if (isFirstRender && props.code === 401) {
      history.push('/');
    }
    setCanShow(true);
  }, [isFirstRender, history, props.code]);

  return canShow ? (
    <Result
      status="warning"
      title={props.code}
      subTitle={message}
      extra={
        <Button type="primary" onClick={(): void => btnClick(props.code)}>
          {props.code === 401 ? '登录' : '返回'}
        </Button>
      }
    />
  ) : null;
}
