import { render } from '@testing-library/react';
import React from 'react';

import { App } from './app';

test('renders learn react link', (): void => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
