import styled from 'styled-components';

// tslint:disable-next-line: variable-name
export const HeaderStyleWrap = styled.div`
  .ant-layout-header {
    border-bottom: solid 1px rgba(204, 204, 204, 0.8);
    padding: 0;
    background-color: #fff;
  }

  .b {
    &-brand {
      font-weight: 600;
      font-size: 16px;
      color: #262626;

      &-divider:before {
        content: '|';
        position: absolute;
        top: -2px;
        display: block;
        margin-left: -18px;
        font-size: 24px;
      }

      &:hover {
        border-bottom: 2px solid #333;
        padding-bottom: 3px;
        width: auto;
      }
    }

    &-cat-logo {
      margin-left: 32px;
      padding-right: 40px;
    }

    &-header,
    &-header-left,
    &-header-right {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    &-header {
      &-left {
        padding: 0 30px;
      }

      &-bar-item {
        width: 64px;
        height: 64px;
        font-size: 16px;
        text-align: center;

        &:hover {
          background-color: rgba(0, 0, 0, 0.04);
          cursor: pointer;
        }
      }
    }

    &-img {
      margin-top: -5px;
      max-width: none;
      width: 135px;
    }

    &-logo {
      height: 100%;
    }
  }
`;

// tslint:disable-next-line: variable-name
export const NavStyleWrap = styled.div`
  .ant-menu-horizontal {
    border: 0;
    line-height: 23px;

    a {
      color: rgba(0, 0, 0, 0.65);
    }

    a:hover,
    .ant-menu-item-selected > a {
      color: black;
    }

    > .ant-menu-item {
      margin: 0 18px;
      padding: 0;
      transition: none;
    }

    .ant-menu-item:hover,
    .ant-menu-item-selected,
    .ant-menu-horizontal {
      border-bottom: 2px solid black;
      color: black;
    }
  }
`;
