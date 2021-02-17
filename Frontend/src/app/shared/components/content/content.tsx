import { Layout, Menu } from 'antd';
import React, { PropsWithChildren, ReactElement } from 'react';
import { withRouter, RouteComponentProps } from 'react-router';

import { Main } from '../../../core/components/main';
import { SidebarStyleWrap } from '../../data-models/content.styles';
import { NavConfig } from '../../data-models/nav-config';

import { RenderSidebar } from './sidebar';

function Content(props: PropsWithChildren<RouteComponentProps>): ReactElement {
  // TODO: Remove mock sidebar below.
  const config: NavConfig = [
    {
      module: 'permission',
      displayName: '权限管理',
      sub: [
        {
          feature: 'user',
          displayName: '用户管理',
          sub: [
            { feature: 'user1', displayName: 'user1' },
            { feature: 'user2', displayName: 'user2' }
          ]
        }
      ]
    },
    {
      module: 'testPage',
      displayName: 'Test Page',
      sub: [
        {
          feature: 'test1',
          displayName: 'test1',
          sub: [{ feature: 'config1', displayName: 'config1' }]
        },
        {
          feature: 'test2',
          displayName: 'test2'
        }
      ]
    }
  ];
  const sidebar = config.filter((c): boolean => c.module === props.match.url.replace('/', ''));

  return (
    <Layout>
      {sidebar.length && sidebar[0].sub ? (
        <SidebarStyleWrap style={{ borderRight: '1px solid rgba(204, 204, 204, 0.8)' }}>
          <Layout.Sider>
            <Menu
              selectedKeys={[props.location.pathname]}
              defaultOpenKeys={[props.location.pathname.replace(/\/([^/]*)$/, '')]}
              mode="inline"
            >
              {RenderSidebar(sidebar[0].sub || [], props.match.url)}
            </Menu>
          </Layout.Sider>
        </SidebarStyleWrap>
      ) : null}
      <Layout.Content>
        <Main children={props.children} />
      </Layout.Content>
    </Layout>
  );
}

const content = withRouter(Content);
export { content as Content };
