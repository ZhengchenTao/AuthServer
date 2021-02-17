import { MailOutlined } from '@ant-design/icons';
import { Menu } from 'antd';
import { User } from 'oidc-client';
import React, { ReactElement } from 'react';
import { useSelector } from 'react-redux';
import { withRouter, Link, RouteComponentProps } from 'react-router-dom';

import Log from '../../../../beyondsoft.png';
import { NavConfig } from '../../../shared/data-models/nav-config';
import { NavStyleWrap } from '../../shared/data-models/header.styles';
import { RootStates } from '../../shared/services/store';

import { MeControl } from './mectrl';

interface HeaderProps extends RouteComponentProps {
  config?: NavConfig;
}

function Header(props: HeaderProps): ReactElement {
  const user = useSelector((state: RootStates): User | undefined => state.oidc.user);

  return (
    <div className="b-header">
      <div className="b-header-left">
        <a className="b-logo" href="https://www.beyondsoft.com/" target="_blank" rel="noopener noreferrer">
          <img src={Log} alt="博彦科技" title="博彦科技" className="b-img" />
        </a>
        <div className="b-brand-divider b-logo b-cat-logo">
          <Link to="/permission" className="b-brand" title="New Project">
            管理系统
          </Link>
        </div>
        <NavStyleWrap>
          <Menu theme="light" mode="horizontal" selectedKeys={[props.match.url]}>
            <Menu.Item key="/permission">
              <Link to="/permission">权限管理</Link>
            </Menu.Item>
            <Menu.Item key="/testPage">
              <Link to="/testPage">Test Page</Link>
            </Menu.Item>
          </Menu>
        </NavStyleWrap>
      </div>
      <div className="b-header-right">
        <div className="b-header-bar-item" title="联系我们">
          <MailOutlined />
        </div>
        <MeControl currentUser={user} />
      </div>
    </div>
  );
}

const header = withRouter(Header);
export { header as Header };
