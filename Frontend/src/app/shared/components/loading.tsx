import React, { ReactElement } from 'react';

import { LoadingStyleWrap } from '../../core/shared/data-models/loading.styles';

export function Loading(): ReactElement {
  return (
    <LoadingStyleWrap>
      <div className="loading loading-container">
        <div className="loading-circle" />
      </div>
    </LoadingStyleWrap>
  );
}
