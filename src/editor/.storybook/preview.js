import "!style-loader!css-loader!sass-loader!react-flow-renderer/dist/style.css"
import "../styles/theme-custom.scss"
import "../styles/globals.scss"
import { themes } from '@storybook/theming';


const lightTheme = {
  base: 'light',
  brandTitle: 'Flooq Storybook',
  brandUrl: 'https://flooq.io',
  brandImage: 'https://raw.githubusercontent.com/Agile-Waterfall-Inc/flooq/bb31404176f6bd5d356316e73ddb2e6f7617ab26/docs/assets/Logo.svg',
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
}

const darkTheme = {
  base: 'dark',
  brandTitle: 'Flooq Storybook',
  brandUrl: 'https://flooq.io',
  brandImage: 'https://raw.githubusercontent.com/Agile-Waterfall-Inc/flooq/bb31404176f6bd5d356316e73ddb2e6f7617ab26/docs/assets/Logo.svg',
  appBg: '#111827',
  appContentBg: '#111827',

  textColor: '#ffffff',
  textInverseColor: '#ffffff',

  inputBg: '#ffffff',
  inputBorder: '#e5e8ec',
  inputTextColor: '#111827',
  inputBorderRadius: 2,

  colorPrimary: '#1f2937',
  colorSecondary: '#3B82F6',
}

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
  },
  darkMode: {
    dark: {...themes.dark, ...darkTheme},
    light: {...themes.light, ...lightTheme},
    stylePreview: true
  },
}