import { render, screen } from '@testing-library/react';
import App from './app';

test('renders learn react list-link', () => {
  render(<App />);
  const linkElement = screen.getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
