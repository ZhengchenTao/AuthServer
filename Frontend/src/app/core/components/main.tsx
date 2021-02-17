import React, { PropsWithChildren, ReactElement } from 'react';

export function Main(props: PropsWithChildren<any>): ReactElement {
  const main = document.createElement('main');
  return main instanceof HTMLUnknownElement ? <div role="main">{props.children}</div> : <>{props.children}</>;
}
