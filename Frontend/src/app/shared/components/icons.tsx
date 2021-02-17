import { SettingOutlined, TeamOutlined } from '@ant-design/icons';
import React, { CSSProperties, ReactElement } from 'react';

export function RenderIcon(iconText: string, style?: CSSProperties): ReactElement | null {
  switch (iconText) {
    case 'permission':
      return <SettingOutlined style={style} />;
    case 'user':
      return <TeamOutlined style={style} />;
    case 'test1':
      return <SettingOutlined style={style} />;
    case 'test2':
      return <SettingOutlined style={style} />;
    default:
      return null;
  }
}
