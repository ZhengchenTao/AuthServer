import { HelmetProps } from 'react-helmet';

import { setHelmetConfig } from '../../shared/services/helmet';

import { HelmetActionTypes, HelmetConfigUpdateAction } from './helmet-actions';

const initialState: HelmetProps = setHelmetConfig('/');

export function helmetReducer(state: HelmetProps = initialState, action: HelmetConfigUpdateAction): HelmetProps {
  if (action.type === HelmetActionTypes.ConfigUpdate) {
    return setHelmetConfig(action.payload);
  } else {
    return state;
  }
}
