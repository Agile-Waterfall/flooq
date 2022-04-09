const path = require('path')

module.exports = {
  "stories": [
    "../stories/**/*.stories.mdx",
    "../stories/**/*.stories.@(js|jsx|ts|tsx)"
  ],
  "addons": [
    "@storybook/addon-links",
    "@storybook/addon-essentials",
    "@storybook/addon-interactions",
    "@storybook/addon-postcss",
    "@storybook/addon-knobs",
    "storybook-addon-sass-postcss",
    'storybook-addon-next',
    'storybook-dark-mode'
  ],
  "framework": "@storybook/react",
  "core": {
    "builder": "webpack5"
  },
  webpackFinal: async (config) => {
    config.module.rules.push({
      test: /\.css$/,
      include: path.resolve(__dirname, '/node_modules/@uiw/react-textarea-code-editor/'),
      use: ["style-loader", "css-loader"]
    })
    return config
  },
}


