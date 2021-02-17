import { Layout } from 'antd';
import { flowRight } from 'lodash';
import React, { useEffect, ComponentType, ReactElement, Suspense } from 'react';
import { withRouter, Route, RouteComponentProps, Switch } from 'react-router';

import { Permission } from '../../permission/permission';
import { Content } from '../../shared/components/content/content';
import { ErrorPage } from '../../shared/components/error-page';
import { Loading } from '../../shared/components/loading';
import { getPageTitles } from '../../shared/data-models/path';
import { withAuth } from '../../shared/hoc/with-auth';
import { authGuard } from '../../shared/utilities/auth-guard';
import { TestPage } from '../../test-page/test-page';
import { HeaderStyleWrap } from '../shared/data-models/header.styles';
import { addHelmetConfig, splitPathname } from '../shared/services/helmet';

import { Header } from './header/header';

const permission = React.lazy(
  (): Promise<{ default: ComponentType<any> }> =>
    import('../../permission/permission').then((): { default: ComponentType<any> } => ({ default: Permission }))
);
const testPage = React.lazy(
  (): Promise<{ default: ComponentType<any> }> =>
    import('../../test-page/test-page').then((): { default: ComponentType<any> } => ({ default: TestPage }))
);

function PrivatePage(props: RouteComponentProps): ReactElement | null {
  useEffect((): void | (() => void | undefined) => {
    // TODO: Add to each page.
    const seg = splitPathname(props.location.pathname).pop();
    addHelmetConfig(`${props.location.pathname}`, {
      [`${seg}`]: {
        title: getPageTitles(seg)
      }
    });
  }, [props.location.pathname]);

  return (
    <Layout style={{ height: '100vh' }}>
      <HeaderStyleWrap>
        <Layout.Header>
          <Header />
        </Layout.Header>
      </HeaderStyleWrap>
      <Suspense fallback={<Loading />}>
        <Content>
          <Switch>
            <Route exact={true} path="/permission" component={permission} />
            <Route exact={true} path="/testPage" component={testPage} />
            <Route key="*" path={`${props.match.url}/*`} children={<ErrorPage code={404} />} />
          </Switch>
        </Content>
      </Suspense>
    </Layout>
  );
}

const privatePage = flowRight(withRouter, withAuth<RouteComponentProps>([authGuard], <ErrorPage code={401} />))(PrivatePage);
export { privatePage as PrivatePage };
