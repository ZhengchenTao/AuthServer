import { HelmetProps } from 'react-helmet';

import { HelmetConfigUpdateAction } from '../../redux/helmet/helmet-actions';

import { store } from './store';

export interface HelmetConfigEntry extends HelmetProps {
  sub?: HelmetConfig;
}

export interface HelmetConfig {
  [path: string]: HelmetConfigEntry;
}

// tslint:disable-next-line:no-var-keyword prefer-const
var helmetConfig: HelmetConfig;

function getHelmetConfig(): HelmetConfig {
  if (!helmetConfig) {
    helmetConfig = {
      '/': {
        title: '管理系统',
        sub: {
          login: {
            title: 'Working...'
          },
          logout: {
            title: 'Working...'
          }
        }
      }
    };
  }
  return helmetConfig;
}

export function splitPathname(pathname: string): string[] {
  const pathSegments = pathname.split('/').filter((segment): boolean => !!segment);
  return pathSegments;
}

export function setHelmetConfig(pathname: string): HelmetProps {
  return splitPathname(pathname).reduce(
    (pre, cur): HelmetConfigEntry => (pre.sub && pre.sub[cur] ? pre.sub[cur] : pre),
    getHelmetConfig()['/']
  );
}

export function addHelmetConfig(pathname: string, config: HelmetConfig): void {
  /* locator */
  const pathSegments = splitPathname(pathname);
  pathSegments.reduce(
    (pre, cur, i): HelmetConfigEntry =>
      i < pathSegments.length - 1
        ? pre.sub
          ? pre.sub[cur]
            ? pre.sub[cur]
            : (pre.sub[cur] = {})
          : (pre.sub = {
              [cur]: {}
            })[cur]
        : pre.sub
        ? (pre.sub[cur] = config[cur])
        : (pre.sub = {
            [cur]: config[cur]
          }),
    getHelmetConfig()['/']
  );
  store.dispatch(new HelmetConfigUpdateAction(pathname));
}
