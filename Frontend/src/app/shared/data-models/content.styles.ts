import styled from 'styled-components';

// tslint:disable-next-line: variable-name
export const SidebarStyleWrap = styled.div`
  .ant-layout-sider {
    position: relative;
    min-width: 0;

    &-children {
      flex: auto 1 1;
      overflow-y: auto;
      overflow-x: hidden;

      > ul {
        height: calc(100vh - 64px);
      }
    }
  }

  .ant-menu-inline {
    border: 0;
  }

  .ant-layout-sider,
  .ant-layout-sider-children > *:last-child,
  .ant-menu {
    background-color: rgb(234, 234, 234);
  }

  .ant-menu:not(.ant-menu-horizontal) .ant-menu-item-selected,
  .ant-menu-item:hover,
  .ant-menu-submenu-active,
  .ant-menu-submenu-title:hover {
    background-color: rgb(206, 206, 206);
    color: black;
  }

  .ant-menu-submenu-selected,
  a:hover,
  .ant-menu-item-selected > a,
  .ant-menu-item > a:hover {
    color: black;
  }

  .ant-menu-sub {
    background-color: rgb(220, 220, 220);
  }

  .ant-menu-submenu-inline > .ant-menu-submenu-title .ant-menu-submenu-arrow:after,
  .ant-menu-submenu-inline > .ant-menu-submenu-title .ant-menu-submenu-arrow:before,
  .ant-menu-inline .ant-menu-item:after {
    content: none;
  }
`;
