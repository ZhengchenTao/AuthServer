import './polyfills'; // Polyfills must be on top of imports.
import React from 'react'; // tslint:disable-line:ordered-imports
import ReactDOM from 'react-dom';

import ConfigProvider from 'antd/es/config-provider';
import zhCN from 'antd/es/locale/zh_CN';
import 'moment/locale/zh-cn';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { OidcProvider } from 'redux-oidc';
import { App } from './app/app';
import { store } from './app/core/shared/services/store';
import { userManager } from './app/core/shared/services/user-manager';
import './index.scss';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
  <Provider store={store}>
    <BrowserRouter>
      <OidcProvider store={store} userManager={userManager}>
        <ConfigProvider locale={zhCN}>
          <App />
        </ConfigProvider>
      </OidcProvider>
    </BrowserRouter>
  </Provider>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
