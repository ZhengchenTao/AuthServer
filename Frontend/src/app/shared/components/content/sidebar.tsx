import { Menu } from 'antd';
import React, { ReactElement } from 'react';
import { Link } from 'react-router-dom';

import { SidebarConfig } from '../../data-models/nav-config';
import { RenderIcon } from '../icons';

export function RenderSidebar(config: SidebarConfig, parent: string): ReactElement[] {
  return config.map(
    (item): ReactElement => {
      if (item.sub) {
        return (
          <Menu.SubMenu
            key={`${parent}/${item.feature}`}
            title={
              <>
                {RenderIcon(item.feature)}
                <span>{item.displayName}</span>
              </>
            }
          >
            {RenderSidebar(item.sub, `${parent}/${item.feature}`)}
          </Menu.SubMenu>
        );
      } else {
        return (
          <Menu.Item key={`${parent}/${item.feature}`}>
            <Link to={{ pathname: `${parent}/${item.feature}` }}>
              {RenderIcon(item.feature)}
              <span>{item.displayName}</span>
            </Link>
          </Menu.Item>
        );
      }
    }
  );
}
