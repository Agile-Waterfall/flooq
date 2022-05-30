import { create } from '@storybook/theming';

export default create({
  base: 'light',
  brandTitle: 'Flooq Storybook',
  brandUrl: 'https://flooq.io',
  brandImage: 'https://raw.githubusercontent.com/agile-waterfall/flooq/bb31404176f6bd5d356316e73ddb2e6f7617ab26/docs/assets/Logo.svg',
  appBg: '#ffffff',
  appContentBg: '#ffffff',

  textColor: '#111827',
  textInverseColor: '#ffffff',

  inputBg: '#ffffff',
  inputBorder: '#e5e8ec',
  inputTextColor: '#111827',
  inputBorderRadius: 2,

  colorPrimary: '#1f2937',
  colorSecondary: '#3B82F6',
});