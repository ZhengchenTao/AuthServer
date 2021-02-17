import { UserManager } from 'oidc-client';
import React, { ReactElement } from 'react';

export function Logout(props: { userManager: UserManager }): ReactElement {
  props.userManager.signoutRedirect();
  return <></>;
}
