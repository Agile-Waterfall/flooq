import "!style-loader!css-loader!sass-loader!react-flow-renderer/dist/style.css"
import "!style-loader!css-loader!sass-loader!@uiw/react-textarea-code-editor/dist.css"
import "../styles/theme-custom.scss"
import "../styles/globals.scss"

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
  },
}