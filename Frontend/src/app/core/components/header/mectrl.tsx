import { UserOutlined } from '@ant-design/icons';
import { Avatar, Dropdown, Menu } from 'antd';
import { User } from 'oidc-client';
import React, { ReactElement } from 'react';
import { Link } from 'react-router-dom';

export function MeControl(props: { currentUser: User | undefined }): ReactElement {
  const avatarMenu = props.currentUser ? (
    <Menu className="mectrl-container">
      <div className="mectrl-info">
        <Avatar size={60} src={undefined} icon={<UserOutlined />} />
        <div className="mectrl-text">{props.currentUser.profile.name}</div>
        <div className="mectrl-text">{props.currentUser.profile.email}</div>
      </div>
      <Menu.Item>
        <Link to="/logout" title="退出账户">
          退出账户
        </Link>
      </Menu.Item>
    </Menu>
  ) : (
    <></>
  );

  return (
    <Dropdown className="b-header-bar-item" overlay={avatarMenu} trigger={['click']} placement="bottomCenter" align={{ offset: [0, 0] }}>
      {props.currentUser ? (
        <div title={props.currentUser.profile.name}>
          <Avatar src={undefined} icon={<UserOutlined />} />
        </div>
      ) : (
        <></>
      )}
    </Dropdown>
  );
}
